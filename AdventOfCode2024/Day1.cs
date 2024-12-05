namespace AdventOfCode2024;

static class Day1
{
    public static void ExecutePart1()
    {
        var instructions = ResourceLoader.LoadText("Day1.txt");
        var values = instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                 .Select(v => v.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                                 .Select(v => new { Left = int.Parse(v[0]), Right = int.Parse(v[1]) });

        var sortedLeftValues = values.Select(v => v.Left).Order().ToArray();
        var sortedRightValues = values.Select(v => v.Right).Order().ToArray();

        var total = sortedLeftValues.Select((v, i) => Math.Abs(sortedRightValues[i] - v)).Sum();
        Console.WriteLine(total);
    }

    public static void ExecutePart2()
    {
        var instructions = ResourceLoader.LoadText("Day1.txt");
        var values = instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                 .Select(v => v.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                                 .Select(v => new { Left = int.Parse(v[0]), Right = int.Parse(v[1]) });

        var leftValues = values.Select(v => v.Left).ToArray();
        var rightValues = values.Select(v => v.Right).GroupBy(v => v).ToDictionary(g => g.Key, g => g.Count());

        var total = leftValues.Select(v => rightValues.GetValueOrDefault(v, defaultValue: 0) * v).Sum();
        Console.WriteLine(total);
    }
}
