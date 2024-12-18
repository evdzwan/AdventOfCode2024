﻿using System.Text.RegularExpressions;

namespace AdventOfCode2024;

partial class Day03() : Day<int>(161, 48)
{
    protected override int ExecutePart1(string input, bool example) => MultiplyRegex().Matches(input).Sum(Multiply);

    protected override int ExecutePart2(string input, bool example)
    {
        return input.Split("do()", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Sum(p => MultiplyRegex().Matches(p.Contains("don't()") ? p[..p.IndexOf("don't()")] : p).Sum(Multiply));
    }

    static int Multiply(Match match) => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);

    [GeneratedRegex(@"mul\x28(\d{1,3}),(\d{1,3})\x29")]
    private static partial Regex MultiplyRegex();
}
