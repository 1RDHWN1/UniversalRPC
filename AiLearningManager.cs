using System.Collections.Concurrent;

public static class AiLearningManager
{
    private static readonly ConcurrentDictionary<
        string,
        bool
    > PendingApps = new();

    public static async Task LearnApp(
        UnknownApp app
    )
    {
        if (
            PendingApps.ContainsKey(
                app.ProcessName
            )
        )
            return;

        PendingApps[
            app.ProcessName
        ] = true;

        try
        {
            Console.WriteLine(
                $"[AI] Learning {app.ProcessName}"
            );

            string category =
                await GeminiClassifier
                    .Classify(app);

            var apps =
                AppDatabase.Load();

      apps[app.ProcessName] =
    new AppEntry
    {
        DisplayName = app.ProductName,
        Company = app.CompanyName,
        Category = category
    };

            AppDatabase.Save(apps);
            UnknownAppDatabase.Remove(
    app.ProcessName
);

            Console.WriteLine(
                $"[AI] {app.ProcessName} => {category}"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[AI ERROR] {ex.Message}"
            );
        }
        finally
        {
            PendingApps.TryRemove(
                app.ProcessName,
                out _
            );
        }
    }
}