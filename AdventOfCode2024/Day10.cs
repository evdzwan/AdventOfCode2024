namespace AdventOfCode2024;

class Day10() : Day<int>(36, 81)
{
    protected override int ExecutePart1(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.Select(c => c - '0').ToArray()).ToArray();
        return Enumerable.Range(0, matrix.Length).SelectMany(y => Enumerable.Range(0, matrix[y].Length).Select(x => GetTrailheads(matrix, x, y, 0, 9).Distinct().Count())).Sum();
    }

    protected override int ExecutePart2(string input, bool example)
    {
        var matrix = input.Split(Environment.NewLine).Select(r => r.Select(c => c - '0').ToArray()).ToArray();
        return Enumerable.Range(0, matrix.Length).SelectMany(y => Enumerable.Range(0, matrix[y].Length).Select(x => GetTrailheads(matrix, x, y, 0, 9).Length)).Sum();
    }

    static (int X, int Y)[] GetTrailheads(int[][] matrix, int x, int y, int current, int target)
    {
        if (y < 0 || y >= matrix.Length || x < 0 || x >= matrix[y].Length || matrix[y][x] != current)
        {
            return [];
        }

        if (current == target)
        {
            return [(x, y)];
        }

        return [..GetTrailheads(matrix, x - 1, y, current + 1, target),
                ..GetTrailheads(matrix, x, y - 1, current + 1, target),
                ..GetTrailheads(matrix, x + 1, y, current + 1, target),
                ..GetTrailheads(matrix, x, y + 1, current + 1, target)];
    }
}
