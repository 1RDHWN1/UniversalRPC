using System.Text.Json;

public class AppEntry
{
    public string DisplayName { get; set; } = "";
    public string Company { get; set; } = "";
    public string Category { get; set; } = "";
}
public static class AppDatabase
{


    
    private static readonly string FilePath = "apps.json";

   public static Dictionary<string, AppEntry> Load()
{
    if (!File.Exists(FilePath))
        return new Dictionary<string, AppEntry>();

    string json = File.ReadAllText(FilePath);

    var apps =
        JsonSerializer.Deserialize<
            Dictionary<string, AppEntry>
        >(json)
        ?? new Dictionary<string, AppEntry>();

    bool changed = false;

    foreach (var app in apps.Values)
    {
        if (app.Category == "general")
        {
            app.Category = "unknown";
            changed = true;
        }
    }

    if (changed)
    {
        Save(apps);
    }

    return apps;
}

    public static void Save(
        Dictionary<string, AppEntry> apps
    )
    
    {
        string json = JsonSerializer.Serialize(
            apps,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        File.WriteAllText(FilePath, json);
    }

public static void Reload()
{
    Load();
}
    

}