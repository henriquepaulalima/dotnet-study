using Mscc.GenerativeAI;

public class ChatService
{
  private readonly GoogleAI _geminiClient;
  private readonly GenerativeModel _model;
  private ChatSession? _chatSession;
  private readonly string _systemPrompt;

  public ChatService(string apiKey, string modelId, string? customSystemPrompt = null)
  {
    _geminiClient = new GoogleAI(apiKey);

    _systemPrompt = customSystemPrompt ?? @"You are a helpful AI assistant. 
You provide clear, accurate, and concise responses. 
You maintain context throughout the conversation and can help with a wide variety of tasks.";

    // Create the model
    var config = new GenerationConfig
    {
      Temperature = 0.9f,
      MaxOutputTokens = 2048,
    };

    _model = _geminiClient.GenerativeModel(model: modelId, generationConfig: config);

    // Start a chat session (this handles conversation history automatically)
    _chatSession = _model.StartChat();
  }

  public async Task RunAsync()
  {
    DisplayWelcomeMessage();

    while (true)
    {
      var userInput = GetUserInput();

      if (string.IsNullOrWhiteSpace(userInput))
        continue;

      // Handle special commands
      if (HandleSpecialCommands(userInput))
        continue;

      // Process the user's message
      await ProcessUserMessageAsync(userInput);
    }
  }

  private void DisplayWelcomeMessage()
  {
    Console.WriteLine("╔════════════════════════════════════════╗");
    Console.WriteLine("║   AI Agent Chat Terminal (Gemini)      ║");
    Console.WriteLine("╚════════════════════════════════════════╝");
    Console.WriteLine("\nCommands:");
    Console.WriteLine("  • Type your message and press Enter to chat");
    Console.WriteLine("  • 'exit' or 'quit' - Exit the application");
    Console.WriteLine("  • 'clear' - Reset conversation history");
    Console.WriteLine("  • 'history' - Show conversation history");
    Console.WriteLine();
  }

  private string GetUserInput()
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("You: ");
    Console.ResetColor();
    return Console.ReadLine() ?? string.Empty;
  }

  private bool HandleSpecialCommands(string input)
  {
    var command = input.Trim().ToLower();

    switch (command)
    {
      case "exit":
      case "quit":
        Console.WriteLine("\nGoodbye! 👋\n");
        Environment.Exit(0);
        return true;

      case "clear":
        ClearConversation();
        return true;

      case "history":
        DisplayHistory();
        return true;

      default:
        return false;
    }
  }

  private void ClearConversation()
  {
    // Restart the chat session to clear history
    _chatSession = _model.StartChat();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("✓ Conversation history cleared.\n");
    Console.ResetColor();
  }

  private void DisplayHistory()
  {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n⚠ History display feature is not available in this version.");
    Console.WriteLine("Your conversation is being tracked internally by the chat session.\n");
    Console.ResetColor();
  }

  private string GetContentText(Content content)
  {
    if (content.Parts == null || !content.Parts.Any())
      return string.Empty;

    var textParts = new List<string>();
    foreach (var part in content.Parts)
    {
      if (part is Part textPart && !string.IsNullOrEmpty(textPart.Text))
      {
        textParts.Add(textPart.Text);
      }
    }

    return string.Join(" ", textParts);
  }

  private async Task ProcessUserMessageAsync(string userInput)
  {
    if (_chatSession == null)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Chat session not initialized.\n");
      Console.ResetColor();
      return;
    }

    try
    {
      // Show loading indicator
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("Agent: ");
      Console.ResetColor();

      // Send message and get response (chat session handles history)
      var response = await _chatSession.SendMessage(userInput);

      // Try different ways to get the text
      string responseText = "[No response]";

      if (response != null)
      {
        // Method 1: Direct Text property
        if (!string.IsNullOrEmpty(response.Text))
        {
          responseText = response.Text;
        }
        // Method 2: From Candidates
        else if (response.Candidates != null && response.Candidates.Any())
        {
          var firstCandidate = response.Candidates.First();
          if (firstCandidate.Content?.Parts != null && firstCandidate.Content.Parts.Any())
          {
            var parts = firstCandidate.Content.Parts;
            var textParts = new List<string>();

            foreach (var part in parts)
            {
              if (part is Part textPart && !string.IsNullOrEmpty(textPart.Text))
              {
                textParts.Add(textPart.Text);
              }
            }

            if (textParts.Any())
            {
              responseText = string.Join("", textParts);
            }
          }
        }
      }

      // Display the response
      Console.WriteLine($"{responseText}\n");
    }
    catch (Exception ex)
    {
      HandleError(ex);
    }
  }

  private void HandleError(Exception ex)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n✗ Error: {ex.Message}");

    if (ex.InnerException != null)
    {
      Console.WriteLine($"  Inner Error: {ex.InnerException.Message}");
    }

    Console.WriteLine();
    Console.ResetColor();
  }
}