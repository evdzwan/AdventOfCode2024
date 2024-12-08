namespace AdventOfCode2024;

class Day8() : Day<int>(14, 34)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var antinodes = new List<(int X, int Y)>();
        for (var y = 0; y < grid.Length; y++)
        {
            var row = grid[y];
            for (var x = 0; x < row.Length; x++)
            {
                var node = row[x];
                if (node != '.')
                {
                    foreach ((var cx, var cy) in FindCorrespondingNodeLocations(node, x, y, grid))
                    {
                        var (dx, dy) = (cx - x, cy - y);

                        var (lx, ly) = (x - dx, y - dy);
                        if (lx >= 0 && ly >= 0 && lx < row.Length && ly < grid.Length)
                        {
                            antinodes.Add((lx, ly));
                        }

                        var (rx, ry) = (cx + dx, cy + dy);
                        if (rx >= 0 && ry >= 0 && rx < row.Length && ry < grid.Length)
                        {
                            antinodes.Add((rx, ry));
                        }
                    }
                }
            }
        }

        return antinodes.Distinct().Count();
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var antinodes = new List<(int X, int Y)>();
        for (var y = 0; y < grid.Length; y++)
        {
            var row = grid[y];
            for (var x = 0; x < row.Length; x++)
            {
                var node = row[x];
                if (node != '.')
                {
                    antinodes.Add((x, y));
                    foreach ((var cx, var cy) in FindCorrespondingNodeLocations(node, x, y, grid))
                    {
                        var (dx, dy) = (cx - x, cy - y);

                        for (var i = 1; i < Math.Max(row.Length, grid.Length); i++)
                        {
                            var (lx, ly) = (x - dx * i, y - dy * i);
                            if (lx >= 0 && ly >= 0 && lx < row.Length && ly < grid.Length)
                            {
                                antinodes.Add((lx, ly));
                            }
                        }

                        for (var i = 1; i < Math.Max(row.Length, grid.Length); i++)
                        {
                            var (rx, ry) = (cx + dx * i, cy + dy * i);
                            if (rx >= 0 && ry >= 0 && rx < row.Length && ry < grid.Length)
                            {
                                antinodes.Add((rx, ry));
                            }
                        }
                    }
                }
            }
        }

        return antinodes.Distinct().Count();
    }

    static IEnumerable<(int X, int Y)> FindCorrespondingNodeLocations(char node, int nodeX, int nodeY, string[] grid)
    {
        for (var y = 0; y < grid.Length; y++)
        {
            var row = grid[y];
            for (var x = 0; x < row.Length; x++)
            {
                if ((x != nodeX || y != nodeY) && row[x] == node)
                {
                    yield return (x, y);
                }
            }
        }
    }
}
