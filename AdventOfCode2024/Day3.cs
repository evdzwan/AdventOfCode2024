using System.Text.RegularExpressions;

namespace AdventOfCode2024;

static partial class Day3
{
    public static void ExecutePart1()
    {
        var instructions = ResourceLoader.LoadText("Day3.txt");

        var total = MultiplyRegex().Matches(instructions).Sum(Multiply);
        Console.WriteLine(total);
    }

    public static void ExecutePart2()
    {
        var instructions = ResourceLoader.LoadText("Day3.txt");
        var parts = instructions.Split("do()", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var total = parts.Sum(p => MultiplyRegex().Matches(p.Contains("don't()") ? p[..p.IndexOf("don't()")] : p).Sum(Multiply));
        Console.WriteLine(total);
    }

    static int Multiply(Match match) => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);

    [GeneratedRegex(@"mul\x28(\d{1,3}),(\d{1,3})\x29")]
    private static partial Regex MultiplyRegex();
}
