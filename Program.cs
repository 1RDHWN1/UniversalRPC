class Program
{
    static void Main(string[] args)
    { if (
    args.Length > 0 &&
    args[0].Equals(
        "migrate",
        StringComparison.OrdinalIgnoreCase
    )
)
{
    MigrationManager
        .MigrateUnknownApps();

    return;
}
        var rpc =
            new PresenceManager(
                "1514195252326563930"
            );

        string lastTitle = "";

        while (true)
        {
            try
            {
                var info =
                    WindowDetector.GetActiveWindow();

                                    if (
                    info.ProductName.Contains(
                        "Discord",
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    Thread.Sleep(2000);
                    continue;
                }

                if (info.Title != lastTitle)
                {
                    lastTitle = info.Title;

                    rpc.Update(info);

                    Console.Clear();

                    Console.WriteLine(
                        $"Category : {info.Category}"
                    );

                    Console.WriteLine(
                        $"Product  : {info.ProductName}"
                    );

                    Console.WriteLine(
                        $"Title    : {info.Title}"
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Thread.Sleep(2000);
        }
    }
}