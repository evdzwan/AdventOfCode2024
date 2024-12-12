namespace AdventOfCode2024;

class Day12() : Day<int>(1_930, 1_206)
{
    [TakesAWhile]
    protected override int ExecutePart1(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.ToCharArray()).ToArray();
        var visited = new List<(int X, int Y)>();

        var total = 0;
        for (var y = 0; y < matrix.Length; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                if (!visited.Contains((x, y)))
                {
                    var area = new List<(int X, int Y)>();
                    GetArea(matrix, x, y, area);

                    visited.AddRange(area);
                    total += area.Count * GetPerimeter(area);
                }
            }
        }

        return total;
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.ToCharArray()).ToArray();
        var visited = new List<(int X, int Y)>();

        var total = 0;
        for (var y = 0; y < matrix.Length; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                if (!visited.Contains((x, y)))
                {
                    var area = new List<(int X, int Y)>();
                    GetArea(matrix, x, y, area);

                    visited.AddRange(area);
                    total += area.Count * GetSides(area);
                }
            }
        }

        return total;
    }

    static void GetArea(char[][] matrix, int x, int y, List<(int X, int Y)> area)
    {
        if (area.Contains((x, y)))
        {
            return;
        }

        area.Add((x, y));
        if (x > 0 && matrix[y][x - 1] == matrix[y][x])
        {
            GetArea(matrix, x - 1, y, area);
        }
        if (y > 0 && matrix[y - 1][x] == matrix[y][x])
        {
            GetArea(matrix, x, y - 1, area);
        }
        if (x < matrix[y].Length - 1 && matrix[y][x + 1] == matrix[y][x])
        {
            GetArea(matrix, x + 1, y, area);
        }
        if (y < matrix.Length - 1 && matrix[y + 1][x] == matrix[y][x])
        {
            GetArea(matrix, x, y + 1, area);
        }
    }

    static int GetPerimeter(List<(int X, int Y)> area)
    {
        return area.Sum(c => (area.Contains(c with { X = c.X - 1 }) ? 0 : 1) +
                             (area.Contains(c with { Y = c.Y - 1 }) ? 0 : 1) +
                             (area.Contains(c with { X = c.X + 1 }) ? 0 : 1) +
                             (area.Contains(c with { Y = c.Y + 1 }) ? 0 : 1));
    }

    static int GetSides(List<(int X, int Y)> area)
    {
        return area.Where(c => !area.Contains(c with { X = c.X - 1 })).OrderBy(c => c.Y).GroupBy(c => c.X, c => c.Y).Select(g => GroupWhile(g, (a, b) => b - a == 1).Count()).Sum() +
                area.Where(c => !area.Contains(c with { Y = c.Y - 1 })).OrderBy(c => c.X).GroupBy(c => c.Y, c => c.X).Select(g => GroupWhile(g, (a, b) => b - a == 1).Count()).Sum() +
                area.Where(c => !area.Contains(c with { X = c.X + 1 })).OrderBy(c => c.Y).GroupBy(c => c.X, c => c.Y).Select(g => GroupWhile(g, (a, b) => b - a == 1).Count()).Sum() +
                area.Where(c => !area.Contains(c with { Y = c.Y + 1 })).OrderBy(c => c.X).GroupBy(c => c.Y, c => c.X).Select(g => GroupWhile(g, (a, b) => b - a == 1).Count()).Sum();
    }

    static IEnumerable<List<T>> GroupWhile<T>(IEnumerable<T> collection, Func<T, T, bool> condition)
    {
        var previous = collection.First();

        var list = new List<T>([previous]);
        foreach (var current in collection.Skip(1))
        {
            if (!condition(previous, current))
            {
                yield return list;
                list = [];
            }

            list.Add(current);
            previous = current;
        }

        yield return list;
    }
}
