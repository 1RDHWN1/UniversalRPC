public static class ClassifyManager
{
    public static async Task Run()
    {
        var unknownApps =
            UnknownAppDatabase.Load();

        var apps =
            AppDatabase.Load();

        foreach (var app in unknownApps)
        {
            Console.WriteLine(
                $"Classifying {app.ProcessName}..."
            );

            string category =
                await GeminiClassifier.Classify(app);

            apps[app.ProcessName] =
                new AppEntry
                {
                    DisplayName =
                        app.ProductName,

                    Company =
                        app.CompanyName,

                    Category =
                        category
                };

            Console.WriteLine(
                $" -> {category}"
            );
        }

        AppDatabase.Save(apps);

        UnknownAppDatabase.Save(
            new List<UnknownApp>()
        );

        Console.WriteLine(
            "Classification completed."
        );
    }
}