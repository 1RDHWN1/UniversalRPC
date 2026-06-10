using System.Text.Json;

public class AppEntry
{
    public string DisplayName { get; set; } = "";
    public string Company { get; set; } = "";
    public string Category { get; set; } = "unknown";
}

public static class AppDatabase
{
    private static string FilePath = "apps.json";

    public static Dictionary<string, AppEntry> Load()
    {
        if (!File.Exists(FilePath))
            return new();

        string json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<
            Dictionary<string, AppEntry>
        >(json) ?? new();
    }

    public static void Save(
        Dictionary<string, AppEntry> data
    )
    {
        string json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        File.WriteAllText(FilePath, json);
    }
}