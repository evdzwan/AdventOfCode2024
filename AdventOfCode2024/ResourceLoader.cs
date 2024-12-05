namespace AdventOfCode2024;

static class ResourceLoader
{
    public static string LoadText(string resourceName)
    {
        using var stream = typeof(ResourceLoader).Assembly.GetManifestResourceStream($"AdventOfCode2024.Resources.{resourceName}");
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }
}
