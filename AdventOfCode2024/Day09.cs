namespace AdventOfCode2024;

class Day09() : Day<long>(1_928, 2_858)
{
    const long Empty = -1L;

    protected override long ExecutePart1(string input, bool example)
    {
        var disk = input.SelectMany((v, i) => i % 2 == 0 ? Enumerable.Repeat((long)(i / 2), int.Parse(v.ToString())) : Enumerable.Repeat(Empty, int.Parse(v.ToString()))).ToList();
        for (var source = disk.Count - 1; source > 0; source--)
        {
            var sourceValue = disk[source];
            if (sourceValue != Empty && disk.IndexOf(Empty) is { } target)
            {
                if (target < 0 || target >= source)
                {
                    break;
                }

                disk[target] = sourceValue;
                disk[source] = Empty;
            }
        }

        return disk.Where(v => v != Empty).Select((v, i) => v * i).Sum();
    }

    [TakesAWhile]
    protected override long ExecutePart2(string input, bool example)
    {
        var disk = input.SelectMany((v, i) => i % 2 == 0 ? Enumerable.Repeat((long)(i / 2), int.Parse(v.ToString())) : Enumerable.Repeat(Empty, int.Parse(v.ToString()))).ToList();
        for (var source = disk.Count - 1; source > 0; source--)
        {
            var sourceValue = disk[source];
            if (sourceValue != Empty)
            {
                var sourceHead = disk.IndexOf(sourceValue);
                var sourceCount = source - sourceHead + 1;

                if (FindTarget(disk, sourceCount, out var target) && target < source)
                {
                    for (var i = 0; i < sourceCount; i++)
                    {
                        disk[target + i] = disk[sourceHead + i];
                        disk[sourceHead + i] = Empty;
                    }
                }

                source -= (source - sourceHead);
            }
        }

        return disk.Select((v, i) => v != Empty ? v * i : 0).Sum();
    }

    static bool FindTarget(List<long> disk, int count, out int index)
    {
        for (var i = 0; i < disk.Count; i++)
        {
            if (disk[i] == Empty && disk.Skip(i + 1).Take(count - 1).All(v => v == Empty))
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }
}
