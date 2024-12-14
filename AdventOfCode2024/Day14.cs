namespace AdventOfCode2024;

class Day14() : Day<int>(12, 0)
{
    const int POSITION = 0;
    const int VELOCITY = 1;

    const int X = 0;
    const int Y = 1;

    protected override int ExecutePart1(string input, bool example)
    {
        var width = example ? 11 : 101;
        var height = example ? 7 : 103;

        var robots = GetRobots(input);
        for (var i = 0; i < 100; i++)
        {
            foreach (var robot in robots)
            {
                robot[POSITION][X] = (robot[POSITION][X] + robot[VELOCITY][X] + width) % width;
                robot[POSITION][Y] = (robot[POSITION][Y] + robot[VELOCITY][Y] + height) % height;
            }
        }

        return GetSafetyFactor(robots, width, height);
    }

    [TakesAWhile]
    protected override int ExecutePart2(string input, bool example)
    {
        if (example)
        {
            return 0;
        }

        var width = example ? 11 : 101;
        var height = example ? 7 : 103;

        var robots = GetRobots(input);
        for (var seconds = 0; ; seconds++)
        {
            if (IsChristmasTree(robots, width, height))
            {
                return seconds;
            }

            foreach (var robot in robots)
            {
                robot[POSITION][X] = (robot[POSITION][X] + robot[VELOCITY][X] + width) % width;
                robot[POSITION][Y] = (robot[POSITION][Y] + robot[VELOCITY][Y] + height) % height;
            }
        }
    }

    static int[][][] GetRobots(string input)
    {
        return input.Split(Environment.NewLine)
                    .Select(r => r.Split(["p=", "v=", " "], StringSplitOptions.RemoveEmptyEntries)
                                  .Select(c => c.Split(',').Select(int.Parse).ToArray())
                                  .ToArray())
                    .ToArray();
    }

    static int GetSafetyFactor(int[][][] robots, int width, int height)
    {
        return robots.Count(r => r[POSITION][X] < ((width - 1) / 2) && r[POSITION][Y] < ((height - 1) / 2)) *
               robots.Count(r => r[POSITION][X] < ((width - 1) / 2) && r[POSITION][Y] >= ((height + 1) / 2)) *
               robots.Count(r => r[POSITION][X] >= ((width + 1) / 2) && r[POSITION][Y] < ((height - 1) / 2)) *
               robots.Count(r => r[POSITION][X] >= ((width + 1) / 2) && r[POSITION][Y] >= ((height + 1) / 2));
    }

    static bool IsChristmasTree(int[][][] robots, int width, int height)
    {
        var lookup = robots.ToLookup(r => (X: r[POSITION][X], Y: r[POSITION][Y]));
        foreach (var group in lookup)
        {
            var top = group.Key;
            if (ContainsRow(top with { Y = top.Y + 1 }, 3) &&
                ContainsRow(top with { Y = top.Y + 2 }, 5) &&
                ContainsRow(top with { Y = top.Y + 3 }, 7) &&
                ContainsRow(top with { Y = top.Y + 4 }, 9) &&
                ContainsRow(top with { Y = top.Y + 5 }, 5) &&
                ContainsRow(top with { Y = top.Y + 6 }, 7) &&
                ContainsRow(top with { Y = top.Y + 7 }, 9) &&
                ContainsRow(top with { Y = top.Y + 8 }, 11) &&
                ContainsRow(top with { Y = top.Y + 9 }, 13) &&
                ContainsRow(top with { Y = top.Y + 10 }, 9) &&
                ContainsRow(top with { Y = top.Y + 11 }, 11) &&
                ContainsRow(top with { Y = top.Y + 12 }, 13) &&
                ContainsRow(top with { Y = top.Y + 13 }, 15) &&
                ContainsRow(top with { Y = top.Y + 14 }, 17) &&
                ContainsRow(top with { Y = top.Y + 15 }, 13) &&
                ContainsRow(top with { Y = top.Y + 16 }, 15) &&
                ContainsRow(top with { Y = top.Y + 17 }, 17) &&
                ContainsRow(top with { Y = top.Y + 18 }, 19) &&
                ContainsRow(top with { Y = top.Y + 19 }, 21) &&
                ContainsRow(top with { Y = top.Y + 20 }, 3) &&
                ContainsRow(top with { Y = top.Y + 21 }, 3) &&
                ContainsRow(top with { Y = top.Y + 22 }, 3))
            {
                return true;
            }
        }

        return false;

        bool ContainsRow((int X, int Y) coordinate, int size)
        {
            return Enumerable.Range(coordinate.X - (size - 1) / 2, size)
                             .All(x => lookup.Contains(coordinate with { X = x }));
        }
    }
}
