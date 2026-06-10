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
private string GetCategoryIcon(WindowInfo info)
{
    return info.Category switch
    {
        "coding" => "coding",
        "design" => "design",
        "browser" => "browsing",
        "communication" => "communicating",
        "gaming" => "gaming",
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
    }

    _client.SetPresence(new RichPresence
    {
        Details = details,
        State = state,

       Assets = new Assets
{
    LargeImageKey = GetImageKey(info),
    LargeImageText = info.ProductName,

    SmallImageKey = GetCategoryIcon(info),
    SmallImageText = info.Category
},

       Timestamps = new Timestamps
{
    Start = _startTime
}
    });
}
}