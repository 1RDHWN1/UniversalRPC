using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;
class Program
{
static void Main(string[] args)
{
    TrayManager.Initialize();

    Mutex mutex =
        new(
            true,
            "ActivityHub",
            out bool createdNew
        );

    if (!createdNew)
    {
        return;
    }

    if (!StartupManager.IsEnabled())
    {
        StartupManager.Enable();
    }

    IconManager.LoadIcons();

    if (
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

    if (
        args.Length > 0 &&
        args[0].Equals(
            "classify",
            StringComparison.OrdinalIgnoreCase
        )
    )
    {
        ClassifyManager
            .Run()
            .GetAwaiter()
            .GetResult();

        return;
    }

    var rpc =
        new PresenceManager(
            "1514195252326563930"
        );

    Thread worker =
        new Thread(() =>
        {
            string lastTitle = "";

            while (true)
            {
                try
                {
                    var info =
                        WindowDetector
                            .GetActiveWindow();

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

                    if (
                        info.Title !=
                        lastTitle
                    )
                    {
                        lastTitle =
                            info.Title;

                        rpc.Update(
                            info
                        );
                    }
                }
                catch
                {
                }

                Thread.Sleep(2000);
            }
        });

    worker.IsBackground = true;
    worker.Start();

    Application.Run();
}

}