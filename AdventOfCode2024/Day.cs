using System.Text.RegularExpressions;

namespace AdventOfCode2024;

abstract partial class Day
{
    protected virtual string Title => string.Join(" ", TitleRegex().Split(GetType().Name).Where(s => s is { Length: > 0 }));

    public abstract void Execute(bool skipLengthyParts);

    [GeneratedRegex(@"(\D+)(\d+)")]
    private static partial Regex TitleRegex();
}
