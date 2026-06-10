using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public static class WindowDetector
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(
        IntPtr hWnd,
        out uint processId
    );

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(
        IntPtr hWnd,
        StringBuilder text,
        int count
    );

  private static string CleanTitle(string title)
{
    string[] suffixes =
    {
        " - Google Chrome",
        " - Visual Studio Code",
        " - Discord",
        " - Microsoft Edge",
        " - Mozilla Firefox",
        " - Brave"
    };

    foreach (var suffix in suffixes)
    {
        if (title.EndsWith(suffix))
        {
            title = title[..^suffix.Length];
            break;
        }
    }

    return title.Trim();
}

public static WindowInfo GetActiveWindow()
{
    try
    {
        IntPtr hwnd = GetForegroundWindow();

        StringBuilder title = new(1024);
        GetWindowText(hwnd, title, title.Capacity);

        GetWindowThreadProcessId(hwnd, out uint pid);

        Process process =
    Process.GetProcessById((int)pid);

if (
    process.ProcessName.Equals(
        "UniversalRPC",
        StringComparison.OrdinalIgnoreCase
    )
)
{
    return new WindowInfo
    {
        ProcessName = "ignored",
        ProductName = "ignored",
        CompanyName = "",
        Title = "",
        Category = "system"
    };
}

        string productName =
            process.ProcessName;

        string companyName =
            "";

        try
        {
            var info =
                process.MainModule?
                    .FileVersionInfo;

            productName =
                info?.ProductName
                ?? process.ProcessName;

            companyName =
                info?.CompanyName
                ?? "";
        }
        catch
        {
        }

        return new WindowInfo
        {
            ProcessName =
                process.ProcessName,

            ProductName =
                productName,

            CompanyName =
                companyName,

            Title =
                CleanTitle(
                    title.ToString()
                ),

            Category =
                CategoryEngine.GetCategory(
                    process.ProcessName,
                    productName,
                    companyName
                )
        };
    }
    catch
    {
        return new WindowInfo
        {
            ProcessName = "unknown",
            ProductName = "Unknown",
            CompanyName = "",
            Title = "",
            Category = "system"
        };
    }
}}