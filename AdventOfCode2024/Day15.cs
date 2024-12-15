using System.Runtime;

namespace AdventOfCode2024;

class Day15() : Day<int>(10_092, 9_021)
{
    const char BOX = 'O';
    const char BOXLEFT = '[';
    const char BOXRIGHT = ']';
    const char EMPTY = '.';
    const char ROBOT = '@';
    const char WALL = '#';

    const char DOWN = 'v';
    const char LEFT = '<';
    const char RIGHT = '>';
    const char UP = '^';

    protected override int ExecutePart1(string input, bool example)
    {
        var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var map = sections[0].Split(Environment.NewLine).Select(r => r.ToCharArray()).ToArray();
        var directions = sections[1];

        var position = FindRobot(map);
        foreach (var direction in directions)
        {
            position = MoveRobot(map, position, direction);
        }

        return GetGPSCoordinates(map).Sum();
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var map = sections[0].Split(Environment.NewLine).Select(r => r.Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@.").ToCharArray()).ToArray();
        var directions = sections[1];

        var position = FindRobot(map);
        foreach (var direction in directions)
        {
            position = MoveRobot(map, position, direction);
        }

        return GetGPSCoordinates(map).Sum();
    }

    static (int X, int Y) FindRobot(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == ROBOT)
                {
                    return (x, y);
                }
            }
        }

        throw new ArgumentException("Failed to find robot", nameof(map));
    }

    static IEnumerable<int> GetGPSCoordinates(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == BOX || map[y][x] == BOXLEFT)
                {
                    yield return x + y * 100;
                }
            }
        }
    }

    static (int X, int Y) MoveRobot(char[][] map, (int X, int Y) position, char direction)
    {
        switch (direction)
        {
            case LEFT:
                for (var x = position.X - 1; map[position.Y][x] != WALL; x--)
                {
                    if (map[position.Y][x] == EMPTY)
                    {
                        for (var dx = x; dx < position.X; dx++)
                        {
                            map[position.Y][dx] = map[position.Y][dx + 1];
                        }

                        return (position.X - 1, position.Y);
                    }
                }

                break;
            case UP:
                bool CanMoveUp((int X, int Y) pos, List<(int X, int Y)> affected)
                {
                    switch (map[pos.Y][pos.X])
                    {
                        case BOX:
                        case ROBOT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y + 1 }, affected);
                        case BOXLEFT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y + 1 }, affected) && CanMoveUp(pos with { X = pos.X + 1, Y = pos.Y + 1 }, affected);
                        case BOXRIGHT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y + 1 }, affected) && CanMoveUp(pos with { X = pos.X - 1, Y = pos.Y + 1 }, affected);
                        case EMPTY:
                            return true;
                    }

                    return false;
                }

                void MoveUp(List<(int X, int Y)> positions)
                {
                    positions.ForEach(p => map[p.Y + 1][p.X] = map[p.Y][p.X]);
                }

                var affectedPositions = new List<(int X, int Y)>();
                if (CanMoveUp(position, affectedPositions))
                {
                    MoveUp(affectedPositions);
                }

                break;
            case RIGHT:
                for (var x = position.X + 1; map[position.Y][x] != WALL; x++)
                {
                    if (map[position.Y][x] == EMPTY)
                    {
                        for (var dx = x; dx > position.X; dx--)
                        {
                            map[position.Y][dx] = map[position.Y][dx - 1];
                        }

                        return (position.X + 1, position.Y);
                    }
                }

                break;
            case DOWN:

                break;
        }

        return position;
    }
}
