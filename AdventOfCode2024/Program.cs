using AdventOfCode2024;

var days = typeof(Program).Assembly.GetTypes()
                                   .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Day)))
                                   .OrderBy(t => t.Name)
                                   .Select(t => (Day)Activator.CreateInstance(t)!)
                                   .ToArray();

for (var i = 0; i < days.Length; i++)
{
    days[i].Execute(skipLengthyParts: i < days.Length - 1);
}
