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
                bool CanMoveLeft((int X, int Y) pos, List<(int X, int Y)> affected)
                {
                    switch (map[pos.Y][pos.X])
                    {
                        case BOX:
                        case BOXLEFT:
                        case BOXRIGHT:
                        case ROBOT:
                            affected.Add(pos);
                            return CanMoveLeft(pos with { X = pos.X - 1 }, affected);
                        case EMPTY:
                            return true;
                        case WALL:
                            return false;
                    }

                    throw new ArgumentException($"Invalid character found at {pos}: {map[pos.Y][pos.X]}", nameof(pos));
                }

                var affectedLeftPositions = new List<(int X, int Y)>();
                if (CanMoveLeft(position, affectedLeftPositions))
                {
                    foreach (var (x, y) in affectedLeftPositions.OrderBy(p => p.X))
                    {
                        map[y][x - 1] = map[y][x];
                        map[y][x] = EMPTY;
                    };

                    return (position.X - 1, position.Y);
                }

                break;
            case UP:
                bool CanMoveUp((int X, int Y) pos, List<(int X, int Y)> affected)
                {
                    if (affected.Contains(pos))
                    {
                        return true;
                    }

                    switch (map[pos.Y][pos.X])
                    {
                        case BOX:
                        case ROBOT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y - 1 }, affected);
                        case BOXLEFT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y - 1 }, affected) && CanMoveUp(pos with { X = pos.X + 1 }, affected);
                        case BOXRIGHT:
                            affected.Add(pos);
                            return CanMoveUp(pos with { Y = pos.Y - 1 }, affected) && CanMoveUp(pos with { X = pos.X - 1 }, affected);
                        case EMPTY:
                            return true;
                        case WALL:
                            return false;
                    }

                    throw new ArgumentException($"Invalid character found at {pos}: {map[pos.Y][pos.X]}", nameof(pos));
                }

                var affectedUpPositions = new List<(int X, int Y)>();
                if (CanMoveUp(position, affectedUpPositions))
                {
                    foreach (var (x, y) in affectedUpPositions.OrderBy(p => p.Y))
                    {
                        map[y - 1][x] = map[y][x];
                        map[y][x] = EMPTY;
                    };

                    return (position.X, position.Y - 1);
                }

                break;
            case RIGHT:
                bool CanMoveRight((int X, int Y) pos, List<(int X, int Y)> affected)
                {
                    switch (map[pos.Y][pos.X])
                    {
                        case BOX:
                        case BOXLEFT:
                        case BOXRIGHT:
                        case ROBOT:
                            affected.Add(pos);
                            return CanMoveRight(pos with { X = pos.X + 1 }, affected);
                        case EMPTY:
                            return true;
                        case WALL:
                            return false;
                    }

                    throw new ArgumentException($"Invalid character found at {pos}: {map[pos.Y][pos.X]}", nameof(pos));
                }

                var affectedRightPositions = new List<(int X, int Y)>();
                if (CanMoveRight(position, affectedRightPositions))
                {
                    foreach (var (x, y) in affectedRightPositions.OrderByDescending(p => p.X))
                    {
                        map[y][x + 1] = map[y][x];
                        map[y][x] = EMPTY;
                    };

                    return (position.X + 1, position.Y);
                }

                break;
            case DOWN:
                bool CanMoveDown((int X, int Y) pos, List<(int X, int Y)> affected)
                {
                    if (affected.Contains(pos))
                    {
                        return true;
                    }

                    switch (map[pos.Y][pos.X])
                    {
                        case BOX:
                        case ROBOT:
                            affected.Add(pos);
                            return CanMoveDown(pos with { Y = pos.Y + 1 }, affected);
                        case BOXLEFT:
                            affected.Add(pos);
                            return CanMoveDown(pos with { Y = pos.Y + 1 }, affected) && CanMoveDown(pos with { X = pos.X + 1 }, affected);
                        case BOXRIGHT:
                            affected.Add(pos);
                            return CanMoveDown(pos with { Y = pos.Y + 1 }, affected) && CanMoveDown(pos with { X = pos.X - 1 }, affected);
                        case EMPTY:
                            return true;
                        case WALL:
                            return false;
                    }

                    throw new ArgumentException($"Invalid character found at {pos}: {map[pos.Y][pos.X]}", nameof(pos));
                }

                var affectedDownPositions = new List<(int X, int Y)>();
                if (CanMoveDown(position, affectedDownPositions))
                {
                    foreach (var (x, y) in affectedDownPositions.OrderByDescending(p => p.Y))
                    {
                        map[y + 1][x] = map[y][x];
                        map[y][x] = EMPTY;
                    };

                    return (position.X, position.Y + 1);
                }

                break;
        }

        return position;
    }
}
