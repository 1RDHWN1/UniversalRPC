public static class MigrationManager
{
    public static void MigrateUnknownApps()
    {
        var apps = AppDatabase.Load();

        int count = 0;

        foreach (var item in apps)
        {
            if (item.Value.Category != "unknown")
                continue;

            UnknownAppDatabase.Add(
                new UnknownApp
                {
                    ProcessName = item.Key,
                    ProductName = item.Value.DisplayName,
                    CompanyName = item.Value.Company
                }
            );

            count++;
        }

        Console.WriteLine(
            $"Migrated {count} apps."
        );
    }
}