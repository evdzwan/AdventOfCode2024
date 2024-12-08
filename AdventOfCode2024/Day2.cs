namespace AdventOfCode2024;

class Day2() : Day<int>(2, 4)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse));

        return rows.Where(r => Enumerable.SequenceEqual(r, r.Order()) || Enumerable.SequenceEqual(r, r.OrderDescending()))
                   .Select(r => r.Zip(r.Skip(1)).Select(v => Math.Abs(v.First - v.Second)))
                   .Count(r => r.All(d => d is >= 1 and <= 3));
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                      .Select(int.Parse)
                                      .ToArray());

        return rows.Select(r => Enumerable.Range(0, r.Length)
                                          .Select(n => r.Where((r, i) => i != n))
                                          .Prepend(r)
                                          .Where(r => Enumerable.SequenceEqual(r, r.Order()) || Enumerable.SequenceEqual(r, r.OrderDescending()))
                                          .Select(r => r.Zip(r.Skip(1)).Select(v => Math.Abs(v.First - v.Second)))
                                          .Any(r => r.All(d => d is >= 1 and <= 3))).Count(v => v);
    }
}
