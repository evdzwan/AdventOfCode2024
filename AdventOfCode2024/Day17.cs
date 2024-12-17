using System.Text.RegularExpressions;

namespace AdventOfCode2024;

partial class Day17() : Day<string>("4,6,3,5,6,3,5,2,1,0", "")
{
    const int ADV = 0;
    const int BXL = 1;
    const int BST = 2;
    const int JNZ = 3;
    const int BXC = 4;
    const int OUT = 5;
    const int BDV = 6;
    const int CDV = 7;

    static int Combo(int operand, int a, int b, int c) => operand switch
    {
        <= 3 => operand,
        4 => a,
        5 => b,
        6 => c,
        _ => throw new NotSupportedException()
    };

    static int ExecuteInstruction(int pointer, (int, int) instruction, ref int a, ref int b, ref int c, List<int> output)
    {
        var (code, operand) = instruction;
        switch (code)
        {
            case ADV:
                a = (int)(a / Math.Pow(2, Combo(operand, a, b, c)));
                break;
            case BXL:
                b ^= operand;
                break;
            case BST:
                b = Combo(operand, a, b, c) % 8;
                break;
            case JNZ when a != 0:
                return operand / 2;
            case BXC:
                b ^= c;
                break;
            case OUT:
                output.Add(Combo(operand, a, b, c) % 8);
                break;
            case BDV:
                b = (int)(a / Math.Pow(2, Combo(operand, a, b, c)));
                break;
            case CDV:
                c = (int)(a / Math.Pow(2, Combo(operand, a, b, c)));
                break;
        }

        return pointer + 1;
    }

    protected override string ExecutePart1(string input, bool example)
    {
        var (a, b, c, program) = ParseInput(input);
        var pointer = 0;

        var output = new List<int>();
        while (pointer < program.Length)
        {
            pointer = ExecuteInstruction(pointer, program[pointer], ref a, ref b, ref c, output);
        }

        return string.Join(',', output);
    }

    protected override string ExecutePart2(string input, bool example)
    {
        return "0";
    }

    static (int A, int B, int C, (int Code, int Operand)[] Program) ParseInput(string input)
    {
        var instructions = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                .Select(i => i.Split(':', StringSplitOptions.TrimEntries)[1])
                                .ToArray();

        return (int.Parse(instructions[0]),
                int.Parse(instructions[1]),
                int.Parse(instructions[2]),
                ProgramRegex().Matches(instructions[3]).Select(m => (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value))).ToArray());
    }

    [GeneratedRegex(@"(\d),(\d)")]
    private static partial Regex ProgramRegex();
}
