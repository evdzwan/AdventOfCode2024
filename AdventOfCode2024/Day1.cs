namespace AdventOfCode2024;

class Day1() : Day<int>(11, 31)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var values = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(v => v.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                          .Select(v => new { Left = int.Parse(v[0]), Right = int.Parse(v[1]) });

        var sortedLeftValues = values.Select(v => v.Left).Order().ToArray();
        var sortedRightValues = values.Select(v => v.Right).Order().ToArray();

        return sortedLeftValues.Select((v, i) => Math.Abs(sortedRightValues[i] - v)).Sum();
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var values = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(v => v.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                          .Select(v => new { Left = int.Parse(v[0]), Right = int.Parse(v[1]) });

        var leftValues = values.Select(v => v.Left).ToArray();
        var rightValues = values.Select(v => v.Right).GroupBy(v => v).ToDictionary(g => g.Key, g => g.Count());

        return leftValues.Sum(v => rightValues.GetValueOrDefault(v, defaultValue: 0) * v);
    }
}
