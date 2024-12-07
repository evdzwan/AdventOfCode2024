namespace AdventOfCode2024;

class Day6() : Day<int>(41, 6)
{
    protected override int ExecutePart1(string input)
    {
        var matrix = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(r => r.ToList())
                          .ToList();

        var visitedPositions = new List<(int, int)>();
        if (!Traverse(matrix, visitedPositions))
        {
            throw new Exception("Traverse failed.");
        }

        return visitedPositions.Distinct().Count();
    }

    protected override int ExecutePart2(string input)
    {
        var matrix = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(r => r.ToList())
                          .ToList();

        var visitedPositions = new List<(int, int)>();
        Traverse(matrix, visitedPositions);

        var obstructions = new List<(int X, int Y)>();
        foreach ((var x, var y) in visitedPositions.Distinct())
        {
            if (matrix[y][x] == '.')
            {
                matrix[y][x] = '#';
                if (!Traverse(matrix, visitedPositions: null))
                {
                    obstructions.Add((x, y));
                }

                matrix[y][x] = '.';
            }
        }

        return obstructions.Count;
    }

    static void Rotate(ref int dx, ref int dy)
    {
        if (dy == -1)
        {
            dx = 1;
            dy = 0;
        }
        else if (dx == 1)
        {
            dx = 0;
            dy = 1;
        }
        else if (dy == 1)
        {
            dx = -1;
            dy = 0;
        }
        else
        {
            dx = 0;
            dy = -1;
        }
    }

    static bool Traverse(List<List<char>> matrix, List<(int X, int Y)>? visitedPositions)
    {
        var row = matrix.First(r => r.Contains('^'));
        var x = row.IndexOf('^');
        var y = matrix.IndexOf(row);

        var height = matrix.Count;
        var width = row.Count;
        var dy = -1;
        var dx = 0;

        var hits = new Dictionary<(int X, int Y), int>();
        while (true)
        {
            visitedPositions?.Add((x, y));
            if ((x + dx) < 0 || (x + dx) >= width || (y + dy) < 0 || (y + dy) >= height)
            {
                return true;
            }

            if (matrix[y + dy][x + dx] == '#')
            {
                var hit = hits.GetValueOrDefault((x, y), 0) + 1;
                if (hit > 2)
                {
                    return false;
                }

                hits[(x, y)] = hit;
                Rotate(ref dx, ref dy);

                visitedPositions?.RemoveAt(visitedPositions.Count - 1);
            }
            else
            {
                x += dx;
                y += dy;
            }
        }
    }
}
