public static class CategoryEngine
{
    public static string GetCategory(
        string processName,
        string productName
    )
    {
        processName = processName.ToLower();
        productName = productName.ToLower();

        // Coding
        if (processName.Contains("code"))
            return "coding";

        if (productName.Contains("visual studio"))
            return "coding";

        // Design
        if (productName.Contains("canva"))
            return "design";

        if (productName.Contains("photoshop"))
            return "design";

        // Browser
        if (processName.Contains("chrome"))
            return "browser";

        if (processName.Contains("msedge"))
            return "browser";

        if (processName.Contains("firefox"))
            return "browser";

        // Communication
        if (productName.Contains("discord"))
            return "communication";

        if (productName.Contains("telegram"))
            return "communication";

        // Game Development
        if (productName.Contains("unity"))
            return "gamedev";

        if (productName.Contains("unreal"))
            return "gamedev";

        // 3D
        if (productName.Contains("blender"))
            return "3d";

        // Streaming
        if (productName.Contains("obs"))
            return "streaming";

        return "general";
    }
}