using System.Collections.Concurrent;

namespace AdventOfCode2024;

class Day11() : Day<long>(55_312, 65_601_038_650_482)
{
    static void Blink(string value, long count, ConcurrentDictionary<string, long> nextState)
    {
        if (value == "0")
        {
            nextState.AddOrUpdate("1", count, (_, c) => c + count);
        }
        else if (value.Length % 2 == 0)
        {
            var halfLength = value.Length / 2;
            var left = value[..halfLength];
            if (value[halfLength..].TrimStart('0') is not { Length: > 0 } right)
            {
                right = "0";
            }

            nextState.AddOrUpdate(left, count, (_, c) => c + count);
            nextState.AddOrUpdate(right, count, (_, c) => c + count);
        }
        else
        {
            var number = long.Parse(value);
            nextState.AddOrUpdate((number * 2_024).ToString(), count, (_, c) => c + count);
        }
    }

    protected override long ExecutePart1(string input, bool example)
    {
        var values = input.Split(' ');

        var state = new ConcurrentDictionary<string, long>(values.GroupBy(v => v).ToDictionary(g => g.Key, g => g.LongCount()));
        for (var i = 0; i < 25; i++)
        {
            var nextState = new ConcurrentDictionary<string, long>();
            foreach (var (value, count) in state)
            {
                Blink(value, count, nextState);
            }

            state = nextState;
        }

        return state.Sum(n => n.Value);
    }

    protected override long ExecutePart2(string input, bool example)
    {
        var values = input.Split(' ');

        var state = new ConcurrentDictionary<string, long>(values.GroupBy(v => v).ToDictionary(g => g.Key, g => g.LongCount()));
        for (var i = 0; i < 75; i++)
        {
            var nextState = new ConcurrentDictionary<string, long>();
            foreach (var (value, count) in state)
            {
                Blink(value, count, nextState);
            }

            state = nextState;
        }

        return state.Sum(n => n.Value);
    }
}
