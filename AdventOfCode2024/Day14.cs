namespace AdventOfCode2024;

class Day14() : Day<int>(12, 1)
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

    protected override int ExecutePart2(string input, bool example)
    {
        return 0;
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
}
