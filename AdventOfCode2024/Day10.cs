namespace AdventOfCode2024;

class Day10() : Day<int>(36, 81)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.Select(c => c - '0').ToArray()).ToArray();
        var size = matrix.Length;

        return Enumerable.Range(0, size).SelectMany(y => Enumerable.Range(0, size).Select(x => GetTrailheads(matrix, size, x, y, 0, 9).Distinct().Count())).Sum();
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.Select(c => c - '0').ToArray()).ToArray();
        var size = matrix.Length;

        return Enumerable.Range(0, size).SelectMany(y => Enumerable.Range(0, size).Select(x => GetTrailheads(matrix, size, x, y, 0, 9).Count())).Sum();
    }

    static (int X, int Y)[] GetTrailheads(int[][] matrix, int size, int x, int y, int current, int target)
    {
        if (x < 0 || x >= size || y < 0 || y >= size || matrix[y][x] != current)
        {
            return [];
        }

        if (current == target)
        {
            return [(x, y)];
        }

        return [..GetTrailheads(matrix, size, x - 1, y, current + 1, target),
                ..GetTrailheads(matrix, size, x, y - 1, current + 1, target),
                ..GetTrailheads(matrix, size, x + 1, y, current + 1, target),
                ..GetTrailheads(matrix, size, x, y + 1, current + 1, target)];
    }
}
