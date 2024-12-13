namespace AdventOfCode2024;

class Day13() : Day<long>(480, 1)
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
            var tokensForMachine = new List<int>();
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
        foreach (var (machineA, machineB, prize) in machines)
        {
            var tokensForMachine = new List<int>();
            for (var a = 1; a <= 1_000_000; a++)
            {
                var x = machineA[X] * a;
                var y = machineA[Y] * a;

                if (x > prize[X] || y > prize[Y])
                {
                    break;
                }

                for (var b = 1; b <= 1_000_000; b++)
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
}
