using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public static class TrayManager
{
    private static NotifyIcon? _tray;

    public static void Initialize()
    {
        _tray = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Visible = true,
            Text = "ActivityHub"
        };

        ContextMenuStrip menu = new();

        menu.Items.Add(
            "Open apps.json",
            null,
            (_, _) =>
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = "apps.json",
                        UseShellExecute = true
                    }
                );
            }
        );

        menu.Items.Add(
            "Open unknown_apps.json",
            null,
            (_, _) =>
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = "unknown_apps.json",
                        UseShellExecute = true
                    }
                );
            }
        );

        menu.Items.Add(
            "Reload Icons",
            null,
            (_, _) =>
            {
                IconManager.LoadIcons();

                MessageBox.Show(
                    "Icons reloaded.",
                    "ActivityHub"
                );
            }
        );
        
menu.Items.Add(
    "Reload Database",
    null,
    (_, _) =>
    {
        AppDatabase.Reload();

        MessageBox.Show(
            "Database reloaded.",
            "ActivityHub"
        );
    }
);
        menu.Items.Add("-");

        menu.Items.Add(
            "Exit",
            null,
            (_, _) =>
            {
                if (_tray != null)
                {
                    _tray.Visible = false;
                }

                Environment.Exit(0);
            }
        );

        _tray.ContextMenuStrip = menu;

        _tray.DoubleClick += (_, _) =>
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "apps.json",
                    UseShellExecute = true
                }
            );
        };
    }
}