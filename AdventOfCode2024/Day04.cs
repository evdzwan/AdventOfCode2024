﻿namespace AdventOfCode2024;

class Day04() : Day<int>(18, 9)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(r => r.ToCharArray())
                          .ToArray();

        var total = 0;
        for (var y = 0; y < matrix.Length; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                if (matrix[y][x] == 'X')
                {
                    if (x >= 3 && matrix[y][x - 1] == 'M' && matrix[y][x - 2] == 'A' && matrix[y][x - 3] == 'S')
                    {
                        total++;
                    }

                    if (x < matrix[y].Length - 3 && matrix[y][x + 1] == 'M' && matrix[y][x + 2] == 'A' && matrix[y][x + 3] == 'S')
                    {
                        total++;
                    }

                    if (y >= 3 && matrix[y - 1][x] == 'M' && matrix[y - 2][x] == 'A' && matrix[y - 3][x] == 'S')
                    {
                        total++;
                    }

                    if (y < matrix[y].Length - 3 && matrix[y + 1][x] == 'M' && matrix[y + 2][x] == 'A' && matrix[y + 3][x] == 'S')
                    {
                        total++;
                    }

                    if (x >= 3 && y >= 3 && matrix[y - 1][x - 1] == 'M' && matrix[y - 2][x - 2] == 'A' && matrix[y - 3][x - 3] == 'S')
                    {
                        total++;
                    }

                    if (x < matrix[y].Length - 3 && y >= 3 && matrix[y - 1][x + 1] == 'M' && matrix[y - 2][x + 2] == 'A' && matrix[y - 3][x + 3] == 'S')
                    {
                        total++;
                    }

                    if (x >= 3 && y < matrix[y].Length - 3 && matrix[y + 1][x - 1] == 'M' && matrix[y + 2][x - 2] == 'A' && matrix[y + 3][x - 3] == 'S')
                    {
                        total++;
                    }

                    if (x < matrix[y].Length - 3 && y < matrix[y].Length - 3 && matrix[y + 1][x + 1] == 'M' && matrix[y + 2][x + 2] == 'A' && matrix[y + 3][x + 3] == 'S')
                    {
                        total++;
                    }
                }
            }
        }

        return total;
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(r => r.ToCharArray())
                          .ToArray();

        var total = 0;
        for (var y = 1; y < matrix.Length - 1; y++)
        {
            for (var x = 1; x < matrix[y].Length - 1; x++)
            {
                if (matrix[y][x] == 'A'
                    && ((matrix[y - 1][x - 1] == 'M' && matrix[y + 1][x + 1] == 'S') || (matrix[y - 1][x - 1] == 'S' && matrix[y + 1][x + 1] == 'M'))
                    && ((matrix[y - 1][x + 1] == 'M' && matrix[y + 1][x - 1] == 'S') || (matrix[y - 1][x + 1] == 'S' && matrix[y + 1][x - 1] == 'M')))
                {
                    total++;
                }
            }
        }

        return total;
    }
}
