namespace AdventOfCode2024;

class Day7() : Day<ulong>(3_749, 11_387)
{
    protected override ulong ExecutePart1(string input, bool example)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                         .Select(r => r.Split(':'))
                         .Select(p => (Result: ulong.Parse(p[0]), Values: p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(ulong.Parse).ToArray()))
                         .ToArray();

        var total = 0UL;
        foreach (var (result, values) in lines)
        {
            if (Matches(values, result, max: 2))
            {
                total += result;
            }
        }

        return total;
    }

    protected override ulong ExecutePart2(string input, bool example)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                 .Select(r => r.Split(':'))
                                 .Select(p => (Result: ulong.Parse(p[0]), Values: p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(ulong.Parse).ToArray()))
                                 .ToArray();

        var total = 0UL;
        foreach (var (result, values) in lines)
        {
            if (Matches(values, result, max: 3))
            {
                total += result;
            }
        }

        return total;
    }

    static void GeneratePossibleOperators(List<int> operators, int index, int max, List<int[]> target)
    {
        if (index == operators.Count)
        {
            target.Add([.. operators]);
            return;
        }

        for (var i = 0; i < max; i++)
        {
            operators[index] = i;
            GeneratePossibleOperators(operators, index + 1, max, target);
        }
    }

    static bool Matches(ulong[] values, ulong result, int max)
    {
        var operatorsList = new List<int[]>();
        GeneratePossibleOperators(Enumerable.Repeat(0, values.Length - 1).ToList(), 0, max, operatorsList);

        foreach (var operators in operatorsList)
        {
            var value = values[0];
            for (var n = 0; n < operators.Length; n++)
            {
                value = operators[n] switch
                {
                    1 => value * values[n + 1],
                    2 => ulong.Parse($"{value}{values[n + 1]}"),
                    _ => value + values[n + 1]
                };
            }

            if (value == result)
            {
                return true;
            }
        }

        return false;
    }
}
