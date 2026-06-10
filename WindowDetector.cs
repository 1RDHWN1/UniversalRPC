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
    IntPtr hwnd = GetForegroundWindow();

    StringBuilder title = new(1024);
    GetWindowText(hwnd, title, title.Capacity);

    GetWindowThreadProcessId(hwnd, out uint pid);

    Process process = Process.GetProcessById((int)pid);

    var info = process.MainModule?.FileVersionInfo;

    string cleanTitle = CleanTitle(title.ToString());

    return new WindowInfo
    {
        ProcessName = process.ProcessName,
        ProductName = info?.ProductName ?? "",
        CompanyName = info?.CompanyName ?? "",
        Title = cleanTitle,
        Category = CategoryEngine.GetCategory(
            process.ProcessName,
            info?.ProductName ?? ""
        )
    };
}
}