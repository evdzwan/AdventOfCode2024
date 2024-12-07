namespace AdventOfCode2024;

class Day7() : Day<long>(3_749, 0)
{
    protected override long ExecutePart1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                         .Select(r => r.Split(':'))
                         .Select(p => (Result: long.Parse(p[0]), Values: p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray()))
                         .ToArray();

        var total = 0L;
        foreach (var (result, values) in lines)
        {
            var possibleResults = new List<long>();

            ProducePossibleResults(values, possibleResults);
            if (possibleResults.Contains(result))
            {
                total += result;
            }
        }

        return total;
    }

    protected override long ExecutePart2(string input)
    {
        throw new NotImplementedException();
    }

    static void ProducePossibleResults(long[] values, List<long> possibleResults)
    {
        var operatorsLength = values.Length - 1;
        for (var i = 0; i < (1 << operatorsLength); i++)
        {
            var binary = Convert.ToString(i, 2);
            var leadingZeroes = "000000000000"[..(operatorsLength - binary.Length)];
            var operators = leadingZeroes + binary;

            var value = values[0];
            for (var o = 0; o < operatorsLength; o++)
            {
                value = operators[o] switch
                {
                    '0' => value + values[o + 1],
                    _ => value * values[o + 1]
                };
            }

            possibleResults.Add(value);
        }
    }
}
