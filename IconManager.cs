using System.Text.RegularExpressions;

public static class IconManager
{
    private static Dictionary<string, string> _icons = new();

    private static readonly Dictionary<
        string,
        string
    > Aliases = new()
    {
        { "code", "vscode" },
        { "msedge", "edge" },
        { "chatgpt", "chat-gpt" },
        { "obs64", "obs-studio" },
        { "epicgameslauncher", "epic-games" },
        { "robloxplayerbeta", "roblox" },
        { "steamwebhelper", "steam" },
        { "whatsapproot", "whatsapp" },
        { "telegram", "telegram" },
        { "discord", "discord" },
        { "pubg", "pubg" },
        { "tslgame", "pubg" },
        { "ExecPubg", "pubg" },
        { "notepad++", "notepad++" },
        { "winword", "word" },
        { "powerpnt", "power-point" }
        



    };

    public static void LoadIcons()
    {
        _icons.Clear();

        string folder =
            "apps_icon/large_image";

        if (!Directory.Exists(folder))
            return;

        foreach (
            string file in Directory.GetFiles(
                folder,
                "*.png"
            )
        )
        {
            string iconKey =
                Path.GetFileNameWithoutExtension(
                    file
                );

            _icons[
                Normalize(iconKey)
            ] = iconKey;
        }
    }

    public static string GetIconKey(
        string processName,
        string productName
    )
    {
        string process =
            Normalize(processName);

        string product =
            Normalize(productName);

        if (
            Aliases.TryGetValue(
                process,
                out string? alias
            )
        )
        {
            return alias;
        }

        if (
            _icons.TryGetValue(
                process,
                out string? icon
            )
        )
        {
            return icon;
        }

        if (
            _icons.TryGetValue(
                product,
                out icon
            )
        )
        {
            return icon;
        }

        return "default";
    }

    private static string Normalize(
        string text
    )
    {
        return Regex.Replace(
            text.ToLower(),
            @"[^a-z0-9]",
            ""
        );
    }
}