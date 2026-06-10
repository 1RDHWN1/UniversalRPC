using System.Text.Json;

public class Config
{
    public string OpenRouterApiKey { get; set; } = "";
}

public static class ConfigManager
{
    public static Config Load()
    {
        string json =
            File.ReadAllText("config.json");

        return JsonSerializer.Deserialize<Config>(json)
            ?? new Config();
    }
}