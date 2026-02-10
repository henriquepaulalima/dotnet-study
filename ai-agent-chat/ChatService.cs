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

        var config = new GenerationConfig
        {
            Temperature = 0.9f,
            MaxOutputTokens = 2048,
        };

        _model = _geminiClient.GenerativeModel(model: modelId, generationConfig: config);
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

            if (HandleSpecialCommands(userInput))
                continue;

            await ProcessUserMessageAsync(userInput);
        }
    }

    private void DisplayWelcomeMessage()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   AI Agent Chat Terminal (Gemini)      ║");
        Console.WriteLine("║         Streaming Mode Enabled         ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine("\nCommands:");
        Console.WriteLine("  • Type your message and press Enter to chat");
        Console.WriteLine("  • 'exit' or 'quit' - Exit the application");
        Console.WriteLine("  • 'clear' - Reset conversation");
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

            default:
                return false;
        }
    }

    private void ClearConversation()
    {
        _chatSession = _model.StartChat();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("✓ Conversation cleared.\n");
        Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Agent: ");
            Console.ResetColor();

            var responseBuilder = new System.Text.StringBuilder();

            await foreach (var chunk in _chatSession.SendMessageStream(userInput))
            {
                if (chunk?.Text != null)
                {
                    Console.Write(chunk.Text);
                    responseBuilder.Append(chunk.Text);
                }
                else if (chunk?.Candidates != null && chunk.Candidates.Any())
                {
                    var firstCandidate = chunk.Candidates.First();
                    if (firstCandidate.Content?.Parts != null)
                    {
                        foreach (var part in firstCandidate.Content.Parts)
                        {
                            if (part is Part textPart && !string.IsNullOrEmpty(textPart.Text))
                            {
                                Console.Write(textPart.Text);
                                responseBuilder.Append(textPart.Text);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\n");
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