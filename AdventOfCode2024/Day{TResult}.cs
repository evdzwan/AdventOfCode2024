﻿using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

abstract partial class Day<TResult>
{
    protected abstract TResult ExamplePart1Solution { get; }

    protected abstract TResult ExamplePart2Solution { get; }

    protected virtual string Title => string.Join(" ", TitleRegex().Split(GetType().Name).Where(s => s is { Length: > 0 }));

    public void Execute()
    {
        var exampleInput = LoadText($"{GetType().Name}Example.txt");
        var input = LoadText($"{GetType().Name}.txt");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Title);
        Console.ResetColor();

        Console.Write($"Examples are ");
        var examplesPart1Valid = ExecutePart1(exampleInput)?.Equals(ExamplePart1Solution) == true;
        var examplesPart2Valid = ExecutePart2(exampleInput)?.Equals(ExamplePart2Solution) == true;
        Console.ForegroundColor = examplesPart1Valid && examplesPart2Valid ? ConsoleColor.Green : examplesPart1Valid || examplesPart2Valid ? ConsoleColor.Yellow : ConsoleColor.Red;
        Console.WriteLine(examplesPart1Valid && examplesPart2Valid ? "both valid" : examplesPart1Valid || examplesPart2Valid ? "partially valid" : "both invalid");
        Console.ResetColor();

        Console.Write("Part 1: ");
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Measure(() => ExecutePart1(input), out var part1Duration));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({part1Duration}ms)");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
        }
        Console.ResetColor();

        Console.Write("Part 2: ");
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Measure(() => ExecutePart2(input), out var part2Duration));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({part2Duration}ms)");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
        }
        Console.ResetColor();

        Console.WriteLine();
    }

    protected abstract TResult ExecutePart1(string input);

    protected abstract TResult ExecutePart2(string input);

    static string LoadText(string resourceName)
    {
        using var stream = typeof(Day<TResult>).Assembly.GetManifestResourceStream($"AdventOfCode2024.Resources.{resourceName}");
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }

    static T Measure<T>(Func<T> action, out long duration)
    {
        var watch = Stopwatch.StartNew();
        var result = action();
        watch.Stop();

        duration = watch.ElapsedMilliseconds;
        return result;
    }

    [GeneratedRegex(@"(\D+)(\d+)")]
    private static partial Regex TitleRegex();
}
