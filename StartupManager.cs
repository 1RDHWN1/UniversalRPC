using Microsoft.Win32;

public static class StartupManager
{
    private const string AppName =
        "ActivityHub";

    public static bool IsEnabled()
    {
        using RegistryKey? key =
            Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run"
            );

        return key?.GetValue(
            AppName
        ) != null;
    }

    public static void Enable()
    {
        using RegistryKey key =
            Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run",
                true
            )!;

        key.SetValue(
            AppName,
            Environment.ProcessPath!
        );
    }
}