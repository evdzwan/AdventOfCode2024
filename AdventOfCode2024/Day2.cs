namespace AdventOfCode2024;

static class Day2
{
    public static void ExecutePart1()
    {
        var instructions = ResourceLoader.LoadText("Day2.txt");
        var rows = instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                               .Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse));

        var total = rows.Where(r => Enumerable.SequenceEqual(r, r.Order()) || Enumerable.SequenceEqual(r, r.OrderDescending()))
                        .Select(r => r.Zip(r.Skip(1)).Select(v => Math.Abs(v.First - v.Second)))
                        .Count(r => r.All(d => d is >= 1 and <= 3));

        Console.WriteLine(total);
    }

    public static void ExecutePart2()
    {
        var instructions = ResourceLoader.LoadText("Day2.txt");
        var rows = instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                               .Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                             .Select(int.Parse)
                                             .ToArray());

        var total = rows.Select(r => Enumerable.Range(0, r.Length)
                                               .Select(n => r.Where((r, i) => i != n))
                                               .Prepend(r)
                                               .Where(r => Enumerable.SequenceEqual(r, r.Order()) || Enumerable.SequenceEqual(r, r.OrderDescending()))
                                               .Select(r => r.Zip(r.Skip(1)).Select(v => Math.Abs(v.First - v.Second)))
                                               .Any(r => r.All(d => d is >= 1 and <= 3))).Count(v => v);

        Console.WriteLine(total);
    }
}
