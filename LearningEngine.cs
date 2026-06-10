public static class LearningEngine
{
    public static string GuessCategory(
        string processName,
        string productName,
        string companyName
    )
    {
        string text =
            $"{processName} {productName} {companyName}"
            .ToLower();

        // Coding
        if (
            text.Contains("code")
            || text.Contains("visual studio")
            || text.Contains("jetbrains")
        )
            return "coding";

        // Design
        if (
            text.Contains("canva")
            || text.Contains("photoshop")
            || text.Contains("figma")
        )
            return "design";

        // Browser
        if (
            text.Contains("chrome")
            || text.Contains("edge")
            || text.Contains("firefox")
            || text.Contains("brave")
        )
            return "browser";

        // Communication
        if (
            text.Contains("discord")
            || text.Contains("telegram")
            || text.Contains("whatsapp")
        )
            return "communication";

        // Game Dev
        if (
            text.Contains("unity")
            || text.Contains("unreal")
        )
            return "gamedev";

        // 3D
        if (
            text.Contains("blender")
        )
            return "3d";

        // Streaming
        if (
            text.Contains("obs")
            || text.Contains("streamlabs")
        )
            return "streaming";

        return "unknown";
    }
}