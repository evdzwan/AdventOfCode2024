namespace AdventOfCode2024;

class Day13() : Day<long>(480, 957_820_661_194)
{
    const int X = 0;
    const int Y = 1;

    protected override long ExecutePart1(string input, bool example)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var machines = Enumerable.Range(0, rows.Length / 3)
                                 .Select(n => rows.Skip(n * 3).Take(3).ToArray())
                                 .Select(i => (A: i[0].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               B: i[1].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               Prize: i[2].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('=')[1])).ToArray()))
                                 .ToArray();

        var total = 0L;
        foreach (var (machineA, machineB, prize) in machines)
        {
            var tokensForMachine = new List<long>();
            for (var a = 1; a <= 100; a++)
            {
                var x = machineA[X] * a;
                var y = machineA[Y] * a;

                if (x > prize[X] || y > prize[Y])
                {
                    break;
                }

                for (var b = 1; b <= 100; b++)
                {
                    var cx = x + machineB[X] * b;
                    var cy = y + machineB[Y] * b;

                    if (cx == prize[X] && cy == prize[Y])
                    {
                        tokensForMachine.Add(a * 3 + b);
                    }
                    else if (cx > prize[X] || cy > prize[Y])
                    {
                        break;
                    }
                }
            }

            if (tokensForMachine.Count > 0)
            {
                total += tokensForMachine.Min();
            }
        }

        return total;
    }

    protected override long ExecutePart2(string input, bool example)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var machines = Enumerable.Range(0, rows.Length / 3)
                                 .Select(n => rows.Skip(n * 3).Take(3).ToArray())
                                 .Select(i => (A: i[0].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               B: i[1].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               Prize: i[2].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => 1_000_000_0000_000 + int.Parse(j.Split('=')[1])).ToArray()))
                                 .ToArray();

        var total = 0L;
        foreach (var machine in machines)
        {
            total += Math.Min(GetFewestTokensToSpendAFirst(machine), GetFewestTokensToSpendBFirst(machine));
        }

        return total;
    }

    static long GetFewestTokensToSpendAFirst((int[] A, int[] B, long[] Prize) machine)
    {
        var remainderAX = machine.Prize[X] % machine.A[X];
        var remainderAY = machine.Prize[Y] % machine.A[Y];

        var tokens = new List<long>();
        for (var pressesB = 1; pressesB < 10_000; pressesB++)
        {
            var remainderBX = remainderAX * pressesB % machine.B[X];
            var remainderBY = remainderAY * pressesB % machine.B[Y];
            if (remainderBX == 0 && remainderBY == 0)
            {
                var pressesA = (machine.Prize[X] - machine.B[X] * pressesB) / machine.A[X];
                tokens.Add(pressesA * 3 + pressesB);
            }
        }

        return tokens.Count > 0 ? tokens.Min() : 0;
    }

    static long GetFewestTokensToSpendBFirst((int[] A, int[] B, long[] Prize) machine)
    {
        var remainderBX = machine.Prize[X] % machine.B[X];
        var remainderBY = machine.Prize[Y] % machine.B[Y];

        var tokens = new List<long>();
        for (var pressesA = 1; pressesA < 10_000; pressesA++)
        {
            var remainderAX = remainderBX * pressesA % machine.A[X];
            var remainderAY = remainderBY * pressesA % machine.A[Y];
            if (remainderAX == 0 && remainderAY == 0)
            {
                var pressesB = (machine.Prize[X] - machine.A[X] * pressesA) / machine.B[X];
                tokens.Add(pressesA * 3 + pressesB);
            }
        }

        return tokens.Count > 0 ? tokens.Min() : 0;
    }
}
