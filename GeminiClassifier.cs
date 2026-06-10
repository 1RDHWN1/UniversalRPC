using System.Text;
using System.Text.Json;

public static class GeminiClassifier
{
    private static readonly HttpClient Client =
        new();

    public static async Task<string> Classify(
        UnknownApp app
    )
    {
        var config =
            ConfigManager.Load();

        string prompt =
$"""
Classify this software.

Process: {app.ProcessName}
Product: {app.ProductName}
Company: {app.CompanyName}

Reply with ONLY ONE WORD from this list:

coding
design
browser
communication
gaming
gamedev
streaming
3d
ai
utility
system

No explanation.
No markdown.
No extra text.
""";

        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
        };

        string json =
            JsonSerializer.Serialize(request);

        string url =
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={config.GoogleApiKey}";

        var response =
            await Client.PostAsync(
                url,
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                )
            );

        string body =
            await response.Content
                .ReadAsStringAsync();

        Console.WriteLine(body);

        using var doc =
            JsonDocument.Parse(body);

        string result = doc
    .RootElement
    .GetProperty("candidates")[0]
    .GetProperty("content")
    .GetProperty("parts")[0]
    .GetProperty("text")
    .GetString()!
    .Trim()
    .ToLower();

Console.WriteLine(
    $"[AI RAW] {result}"
);

string[] validCategories =
{
    "coding",
    "design",
    "browser",
    "communication",
    "gaming",
    "gamedev",
    "streaming",
    "3d",
    "ai",
    "utility",
    "system"
};

foreach (var category in validCategories)
{
    if (result.Contains(category))
        return category;
}

return "general";

}}