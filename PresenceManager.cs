using DiscordRPC;

public class PresenceManager
{
    private readonly DiscordRpcClient _client;
    private string GetImageKey(WindowInfo info)
    {
        string process =
            info.ProcessName.ToLower();

    string product =
        info.ProductName.ToLower();

    if (product.Contains("canva"))
        return "canva";

    if (process.Contains("code"))
        return "vscode";

    if (process.Contains("chrome"))
        return "chrome";

    return "default";
}
private string SafeText(string text)
{
    if (string.IsNullOrWhiteSpace(text))
        return "";

    return text.Length > 120
        ? text[..120]
        : text;
}

private string GetCategoryIcon(WindowInfo info)
{
    return info.Category switch
    {
        "coding" => "coding",
        "design" => "design",
        "browser" => "browsing",
        "communication" => "communicating",
        "gaming" => "gaming",
        "gamedev" => "gamedev",
        "streaming" => "streaming",
        "3d" => "3d",
        "ai" => "ai",
        "utility" => "utility",
        "system" => "system",
        _ => "default"
    };
}
private DateTime _startTime = DateTime.UtcNow;
private string _lastProcess = "";

    public PresenceManager(string appId)
    {
        _client = new DiscordRpcClient(appId);
        _client.Initialize();
    }

    public void Update(WindowInfo info)
{
    if (_lastProcess != info.ProcessName)
{
    _lastProcess = info.ProcessName;
    _startTime = DateTime.UtcNow;
}
    string details = info.ProductName;
    string state = info.Title;

   switch (info.Category)
{
    case "coding":
        details = "💻 Coding";
        break;

    case "design":
        details = "🎨 Designing";
        break;

    case "browser":
        details = "🌐 Browsing";
        break;

    case "communication":
        details = "💬 Chatting";
        break;

    case "gamedev":
        details = "🎮 Game Development";
        break;

    case "gaming":
        details = "🎮 Gaming";
        break;

    case "ai":
        details = "🤖 AI Assistant";
        break;

    case "utility":
        details = "🛠️ Utility";
        break;

    case "system":
        details = "⚙️ System";
        break;

    case "streaming":
        details = "📺 Streaming";
        break;

    case "3d":
        details = "🧊 3D Modeling";
        break;
}

    _client.SetPresence(new RichPresence
    {
        Details = SafeText(details),
State = SafeText(state),

       Assets = new Assets
{
    LargeImageKey = GetImageKey(info),
    LargeImageText = SafeText(
    info.ProductName
),

    SmallImageKey = GetCategoryIcon(info),
    SmallImageText = SafeText(
    info.Category
)
},

       Timestamps = new Timestamps
{
    Start = _startTime
}
    });
}
}