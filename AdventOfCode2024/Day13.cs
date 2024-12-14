namespace AdventOfCode2024;

class Day13() : Day<long>(480, 875_318_608_908)
{
    const int A = 0;
    const int B = 1;
    const int PRIZE = 2;

    const int X = 0;
    const int Y = 1;

    protected override long ExecutePart1(string input, bool example)
    {
        var total = 0L;
        foreach (var machine in GetMachines(input))
        {
            var d = machine[A][X] * machine[B][Y] - machine[A][Y] * machine[B][X];
            if (d == 0)
            {
                continue;
            }

            var dx = machine[PRIZE][X] * machine[B][Y] - machine[PRIZE][Y] * machine[B][X];
            var dy = machine[A][X] * machine[PRIZE][Y] - machine[A][Y] * machine[PRIZE][X];
            if (dx % d == 0 && dy % d == 0)
            {
                total += dx / d * 3 + dy / d;
            }
        }

        return total;
    }

    protected override long ExecutePart2(string input, bool example)
    {
        const long Offset = 10_000_000_000_000L;

        var total = 0L;
        foreach (var machine in GetMachines(input))
        {
            var d = machine[A][X] * machine[B][Y] - machine[A][Y] * machine[B][X];
            if (d == 0)
            {
                continue;
            }

            var dx = (Offset + machine[PRIZE][X]) * machine[B][Y] - (Offset + machine[PRIZE][Y]) * machine[B][X];
            var dy = machine[A][X] * (Offset + machine[PRIZE][Y]) - machine[A][Y] * (Offset + machine[PRIZE][X]);
            if (dx % d == 0 && dy % d == 0)
            {
                total += dx / d * 3 + dy / d;
            }
        }

        return total;
    }

    static int[][][] GetMachines(string input)
    {
        return input.Split($"{Environment.NewLine}{Environment.NewLine}")
                    .Select(m => m.Split(Environment.NewLine)
                                  .Select(i => i.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
                                                .TakeLast(2)
                                                .Select(c => int.Parse(c.Split(['+', '='])[1]))
                                                .ToArray())
                                  .ToArray())
                    .ToArray();
    }
}
