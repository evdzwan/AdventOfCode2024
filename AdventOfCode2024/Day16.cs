using System.Collections.Concurrent;

namespace AdventOfCode2024;

class Day16() : Day<int>(7_036, 45)
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
        var maze = input.Split(Environment.NewLine).Select(r => r.ToCharArray()).ToArray();
        var (sx, sy) = (1, maze.Length - 2);
        var (ex, ey) = (maze[1].Length - 2, 1);

        var costs = new Dictionary<(int, int, int), int>();
        var from = new ConcurrentDictionary<(int, int, int), (int, int, int)[]>();
        var queue = new PriorityQueue<(int, int, int, int), int>([((sx, sy, 0, 0), 0)]);
        while (queue.Count > 0)
        {
            var (cx, cy, cdir, ccost) = queue.Dequeue();
            if (cx == ex && cy == ey)
            {
                break;
            }

            foreach (var (nx, ny, ndir, ncost) in GetNextMoves(maze, cx, cy, cdir))
            {
                if (ccost + ncost < costs.GetValueOrDefault((nx, ny, ndir), int.MaxValue))
                {
                    costs[(nx, ny, ndir)] = ccost + ncost;
                    queue.Enqueue((nx, ny, ndir, ccost + ncost), ccost + ncost);
                    from.AddOrUpdate((nx, ny, ndir), _ => [(cx, cy, cdir)], (_, l) => [.. l, (cx, cy, cdir)]);
                }
                else if (ccost + ncost <= costs.GetValueOrDefault((nx, ny, ndir), int.MaxValue))
                {
                    from.AddOrUpdate((nx, ny, ndir), _ => [(cx, cy, cdir)], (_, l) => [.. l, (cx, cy, cdir)]);
                }
            }
        }

        var ed = Enumerable.Range(0, 4).MinBy(d => costs.GetValueOrDefault((ex, ey, d), int.MaxValue));
        var nodes = new List<(int X, int Y, int)>([(ex, ey, ed)]);
        var stack = new Stack<(int, int, int)>([(ex, ey, ed)]);
        while (stack.Count > 0)
        {
            var some = stack.Pop();
            foreach (var other in from.GetValueOrDefault(some, []))
            {
                if (!nodes.Contains(other))
                {
                    nodes.Add(other);
                    stack.Push(other);
                }
            }
        }

        return nodes.Select(n => (n.X, n.Y)).Distinct().Count();
        //var beh = nodes.Select(n => (n.X, n.Y)).Distinct().ToArray();
        //var lookup = beh.ToDictionary(p => p);

        //using var writer = File.CreateText(@$"Aap{(example ? "Example" : "")}.txt");
        //for (var y = 0; y < maze.Length; y++)
        //{
        //    for (var x = 0; x < maze[y].Length; x++)
        //    {
        //        if (lookup.ContainsKey((x, y)) && maze[y][x] == '#')
        //        {
        //            throw new InvalidOperationException();
        //        }

        //        writer.Write(lookup.ContainsKey((x, y)) ? 'O' : maze[y][x]);
        //    }
        //    writer.WriteLine();
        //}

        //return beh.Length;
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
