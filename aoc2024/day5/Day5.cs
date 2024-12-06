namespace aoc2024.day5;

public class Day5
{
    public static void SolvePart1()
    {
        var rows = File.ReadAllLines("./day5/input.txt");
        var deps = new Dictionary<int, List<int>>();
        foreach (var dep in rows.Where(r => r.Contains('|')))
        {
            var items = dep.Split('|').Select(i =>Convert.ToInt32(i)).ToList();
            if (deps.ContainsKey(items[0]))
            {
                deps[items[0]].Add(items[1]);
            }
            else
            {
                deps.Add(items[0], new() { items[1]});
            }
        }

        var printRuns = rows
            .Where(r => r.Contains(','))
            .Select(r => r.Split(',')
                .Select(i => Convert.ToInt32(i))
                .ToList())
            .ToList();

        var answer = 0;
        foreach (var printRun in printRuns)
        {
            var valid = true;
            for (var i = printRun.Count-1; i >= 0; i--)
            {
                
                if (printRun[..i].Any(p =>
                    {
                        if (deps.ContainsKey(printRun[i]))
                        {
                            return deps[printRun[i]].Any(d => d == p);
                        }

                        return false;
                    }))
                {
                    valid = false;
                }
            }

            if (valid)
            {
                answer += printRun[(int)Math.Ceiling(printRun.Count / 2.0) - 1];
            }
        }
        Console.WriteLine(answer);
    }
    
    public static void SolvePart2()
    {
        var rows = File.ReadAllLines("./day5/input.txt");
        var deps = new Dictionary<int, List<int>>();
        foreach (var dep in rows.Where(r => r.Contains('|')))
        {
            var items = dep.Split('|').Select(i =>Convert.ToInt32(i)).ToList();
            if (deps.ContainsKey(items[0]))
            {
                deps[items[0]].Add(items[1]);
            }
            else
            {
                deps.Add(items[0], new() { items[1]});
            }
        }

        var printRuns = rows
            .Where(r => r.Contains(','))
            .Select(r => r.Split(',')
                .Select(i => Convert.ToInt32(i))
                .ToList())
            .ToList();

        var answer = 0;
        foreach (var printRun in printRuns)
        {
            var valid = true;
            for (var i = printRun.Count-1; i >= 0; i--)
            {
                
                if (printRun[..i].Any(p =>
                    {
                        if (deps.ContainsKey(printRun[i]))
                        {
                            return deps[printRun[i]].Any(d => d == p);
                        }

                        return false;
                    }))
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                var correct = CorrectOrder(printRun, deps);
                answer += correct[(int)Math.Ceiling(correct.Count / 2.0) - 1];
            }
        }
        Console.WriteLine(answer);
    }

    private static List<int> CorrectOrder(List<int> current, Dictionary<int, List<int>> deps)
    {
        return current.OrderByDescending(x =>
        {
            if (deps.ContainsKey(x))
            {
                return deps[x].Count(d => current.Exists(c => c == d));
            }

            return 0;
        }).ToList();
    }
}