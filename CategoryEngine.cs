public static class CategoryEngine
{
public static string GetCategory(
    string processName,
    string productName,
    string companyName
)
{
    var apps = AppDatabase.Load();

    if (apps.TryGetValue(processName, out var app))
    {
        return app.Category;
    }

    string category =
        LearningEngine.GuessCategory(
            processName,
            productName,
            companyName
        );
if (category == "unknown")
{
    UnknownAppDatabase.Add(
        new UnknownApp
        {
            ProcessName = processName,
            ProductName = productName,
            CompanyName = companyName
        }
    );
}
    apps[processName] = new AppEntry
    {
        DisplayName = productName,
        Company = companyName,
        Category = category
    };

    AppDatabase.Save(apps);

    return category;
}
}