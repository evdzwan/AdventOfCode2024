using AdventOfCode2024;

foreach (var day in typeof(Program).Assembly.GetTypes()
                                            .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Day)))
                                            .OrderBy(t => t.Name)
                                            .Select(t => (Day)Activator.CreateInstance(t)!)
                                            .Skip(7))
{
    day.Execute();
}
