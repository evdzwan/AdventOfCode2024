namespace AdventOfCode2024;

class Day5() : Day<int>(143, 123)
{
    protected override int ExecutePart1(string input)
    {
        var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);

        var rules = parts[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .Select(r => r.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray())
                            .ToLookup(r => r[0], r => r[1]);

        var updates = parts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                              .Select(u => u.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray())
                              .ToArray();

        var correctUpdates = GetCorrectUpdates(updates, rules);
        return correctUpdates.Sum(u => u[(u.Count - 1) / 2]);
    }

    protected override int ExecutePart2(string input)
    {
        var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);

        var rules = parts[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .Select(r => r.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray())
                            .ToLookup(r => r[0], r => r[1]);

        var updates = parts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                              .Select(u => u.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList())
                              .ToArray();

        var correctUpdates = GetCorrectUpdates(updates, rules);
        var incorrectUpdates = updates.Except(correctUpdates).ToArray();
        foreach (var update in incorrectUpdates)
        {
            var iterationRequired = true;
            while (iterationRequired)
            {
                iterationRequired = false;
                foreach (var rule in rules)
                {
                    var matchIndex = update.IndexOf(rule.Key);
                    if (matchIndex >= 0)
                    {
                        foreach (var right in rule)
                        {
                            var rightIndex = update.IndexOf(right);
                            if (rightIndex >= 0 && rightIndex < matchIndex)
                            {
                                update.RemoveAt(matchIndex);
                                update.Insert(rightIndex, rule.Key);
                                iterationRequired = true;
                                break;
                            }
                        }
                    }

                    if (iterationRequired)
                    {
                        break;
                    }
                }
            }
        }

        return incorrectUpdates.Sum(u => u[(u.Count - 1) / 2]);
    }

    static List<IList<int>> GetCorrectUpdates(IEnumerable<IList<int>> updates, ILookup<int, int> rules)
    {
        var correctUpdates = new List<IList<int>>();
        foreach (var update in updates)
        {
            var correct = true;
            for (var i = 0; i < update.Count; i++)
            {
                if (update.Take(i).Intersect(rules[update[i]] ?? []).Any())
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                correctUpdates.Add(update);
            }
        }

        return correctUpdates;
    }
}
