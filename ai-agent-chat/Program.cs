using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var apiKey = configuration["Gemini:ApiKey"]
    ?? throw new InvalidOperationException(
        "Gemini API key not configured. Run: dotnet user-secrets set \"Gemini:ApiKey\" \"your-key\"");

var modelId = configuration["Gemini:ModelId"] ?? "models/gemini-2.5-flash";

Console.WriteLine($"Using model: {modelId}\n");

var chatService = new ChatService(apiKey, modelId);
await chatService.RunAsync();