using System.Text.Json;

public static class UnknownAppDatabase
{
    private static readonly string FilePath =
        "unknown_apps.json";

    public static List<UnknownApp> Load()
    {
        if (!File.Exists(FilePath))
            return new();

        string json =
            File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<
            List<UnknownApp>
        >(json)
        ?? new();
    }

    public static void Save(
        List<UnknownApp> apps
    )
    {
        string json =
            JsonSerializer.Serialize(
                apps,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

        File.WriteAllText(
            FilePath,
            json
        );
    }

    public static void Add(
        UnknownApp app
    )
    {
        var apps = Load();

        bool exists =
            apps.Any(x =>
                x.ProcessName ==
                app.ProcessName
            );

        if (exists)
            return;

        apps.Add(app);

        Save(apps);
    }
}