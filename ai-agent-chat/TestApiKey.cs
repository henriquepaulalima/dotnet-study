using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class ApiKeyTester
{
    public static async Task TestApiKey()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        var apiKey = configuration["Gemini:ApiKey"];
        
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("No API key configured!");
            return;
        }

        Console.WriteLine($"Testing API key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");

        using var client = new HttpClient();
        
        // Try to list models using REST API
        var url = $"https://generativelanguage.googleapis.com/v1/models?key={apiKey}";
        
        try
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Response:\n{content}");
            
            if (response.IsSuccessStatusCode)
            {
                var json = JsonDocument.Parse(content);
                if (json.RootElement.TryGetProperty("models", out var models))
                {
                    Console.WriteLine("\nAvailable models:");
                    foreach (var model in models.EnumerateArray())
                    {
                        if (model.TryGetProperty("name", out var name))
                        {
                            Console.WriteLine($"  - {name.GetString()}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}