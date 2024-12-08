using System.Diagnostics;

namespace AdventOfCode2024;

abstract partial class Day<TResult>(TResult examplePart1Solution, TResult examplePart2Solution) : Day
{
    public override sealed void Execute()
    {
        var exampleInput = LoadText($"{GetType().Name}Example.txt");
        var input = LoadText($"{GetType().Name}.txt");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Title);
        Console.ResetColor();

        Console.Write($"Examples are ");
        try
        {
            var examplesPart1Valid = ExecutePart1(exampleInput, example: true)?.Equals(examplePart1Solution) == true;
            var examplesPart2Valid = ExecutePart2(exampleInput, example: true)?.Equals(examplePart2Solution) == true;
            Console.ForegroundColor = examplesPart1Valid && examplesPart2Valid ? ConsoleColor.Green : examplesPart1Valid || examplesPart2Valid ? ConsoleColor.Yellow : ConsoleColor.Red;
            Console.WriteLine(examplesPart1Valid && examplesPart2Valid ? "all valid" : examplesPart1Valid || examplesPart2Valid ? "partially valid" : "all invalid");
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("all invalid");
        }
        Console.ResetColor();

        Console.Write("Part 1: ");
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Measure(() => ExecutePart1(input, example: false), out var part1Duration));
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
            Console.Write(Measure(() => ExecutePart2(input, example: false), out var part2Duration));
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

    protected abstract TResult ExecutePart1(string input, bool example);

    protected abstract TResult ExecutePart2(string input, bool example);

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
}
