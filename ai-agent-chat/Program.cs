using Microsoft.Extensions.Configuration;

// Build configuration to access user secrets
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Get API key from user secrets
var apiKey = configuration["Gemini:ApiKey"]
    ?? throw new InvalidOperationException(
        "Gemini API key not configured. Run: dotnet user-secrets set \"Gemini:ApiKey\" \"your-key\"");

// Use a model that's available in your account
var modelId = configuration["Gemini:ModelId"] ?? "models/gemini-2.5-flash";

Console.WriteLine($"Using model: {modelId}\n");

// Create and run the chat service
var chatService = new ChatService(apiKey, modelId);
await chatService.RunAsync();