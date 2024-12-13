namespace AdventOfCode2024;

class Day13() : Day<long>(480, 0)
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
                                               Prize: i[2].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => long.Parse(j.Split('=')[1])).ToArray()))
                                 .ToArray();

        return machines.Sum(m => Math.Min(GetMinimalTokenAmountUsingA(m.A, m.B, m.Prize), GetMinimalTokenAmountUsingB(m.A, m.B, m.Prize)));
    }

    [TakesAWhile]
    protected override long ExecutePart2(string input, bool example)
    {
        var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var machines = Enumerable.Range(0, rows.Length / 3)
                                 .Select(n => rows.Skip(n * 3).Take(3).ToArray())
                                 .Select(i => (A: i[0].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               B: i[1].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => int.Parse(j.Split('+')[1])).ToArray(),
                                               Prize: i[2].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries).TakeLast(2).Select(j => 1_000_000_0000_000 + long.Parse(j.Split('=')[1])).ToArray()))
                                 .ToArray();

        return machines.Sum(m => Math.Min(GetMinimalTokenAmountUsingA(m.A, m.B, m.Prize), GetMinimalTokenAmountUsingB(m.A, m.B, m.Prize)));
    }

    static long GetMinimalTokenAmountUsingA(int[] a, int[] b, long[] prize)
    {
        var amount = long.MaxValue;

        for (var countA = Math.Min(prize[X] / a[X], prize[Y] / a[Y]); countA >= 0; countA--)
        {
            var remainderX = (prize[X] - countA * a[X]) % b[X];
            var remainderY = (prize[Y] - countA * a[Y]) % b[Y];

            if (remainderX == 0 && remainderY == 0)
            {
                var countBX = (prize[X] - countA * a[X]) / b[X];
                var countBY = (prize[Y] - countA * a[Y]) / b[Y];
                if (countBX == countBY)
                {
                    var newAmount = countA * 3 + countBX;
                    if (newAmount < amount)
                    {
                        amount = newAmount;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return amount == long.MaxValue ? 0 : amount;
    }

    static long GetMinimalTokenAmountUsingB(int[] a, int[] b, long[] prize)
    {
        var amount = long.MaxValue;
        
        for (var countB = Math.Min(prize[X] / b[X], prize[Y] / b[Y]); countB >= 0;countB--)
        {
            var remainderX = (prize[X] - countB * b[X]) % a[X];
            var remainderY = (prize[Y] - countB * b[Y]) % a[Y];

            if (remainderX == 0 && remainderY == 0)
            {
                var countAX = (prize[X] - countB * b[X]) / a[X];
                var countAY = (prize[Y] - countB * b[Y]) / a[Y];
                if (countAX == countAY)
                {
                    var newAmount = countAX * 3 + countB;
                    if (newAmount < amount)
                    {
                        amount = newAmount;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return amount == long.MaxValue ? 0 : amount;
    }
}
