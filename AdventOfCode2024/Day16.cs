namespace AdventOfCode2024;

class Day16() : Day<int>(7_036, 1)
{
    static readonly (int dx, int dy)[] Directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];
    const char WALL = '#';

    protected override int ExecutePart1(string input, bool example)
    {
        var maze = input.Split(Environment.NewLine).Select(r => r.ToCharArray()).ToArray();
        var (sx, sy) = (1, maze.Length - 2);
        var (ex, ey) = (maze[1].Length - 2, 1);

        var costs = new Dictionary<(int, int, int), int>();
        var stack = new PriorityQueue<(int, int, int, int), int>([((sx, sy, 0, 0), 0)]);
        while (stack.Count > 0)
        {
            var (cx, cy, cdir, ccost) = stack.Dequeue();
            if (cx == ex && cy == ey)
            {
                return ccost;
            }

            foreach (var (nx, ny, ndir, ncost) in GetNextMoves(maze, cx, cy, cdir))
            {
                if (ccost + ncost < costs.GetValueOrDefault((nx, ny, ndir), int.MaxValue))
                {
                    costs[(nx, ny, ndir)] = ccost + ncost;
                    stack.Enqueue((nx, ny, ndir, ccost + ncost), ccost + ncost);
                }
            }
        }

        return 0;
    }

    protected override int ExecutePart2(string input, bool example)
    {
        return 0;
    }

    static IEnumerable<(int, int, int, int)> GetNextMoves(char[][] maze, int cx, int cy, int cdir)
    {
        yield return (cx, cy, (((cdir - 1) % 4) + 4) % 4, 1_000);
        yield return (cx, cy, (cdir + 1) % 4, 1_000);

        var (dx, dy) = Directions[cdir];
        var (nx, ny) = (cx + dx, cy + dy);
        if (maze[ny][nx] != WALL)
        {
            yield return (nx, ny, cdir, 1);
        }
    }
}
