namespace aoc2024.day7;

public class Day7
{
    public static void SolvePart1()
    {
        var equations = File.ReadAllLines("./day7/input.txt");

        long total = 0;
        foreach (var equation in equations)
        {
            var split = equation.Split(":");
            var expected = Convert.ToInt64(split[0]);
            var numbers = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt32(i))
                .ToList();

            if (CanWork(expected, numbers))
            {
                total += expected;
            }
        }

        Console.WriteLine(total);
    }

    public static void SolvePart2()
    {
        var equations = File.ReadAllLines("./day7/input.txt");

        long total = 0;
        foreach (var equation in equations)
        {
            var split = equation.Split(":");
            var expected = Convert.ToInt64(split[0]);
            var numbers = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt32(i))
                .ToList();

            if (CanWork(expected, numbers))
            {
                total += expected;
            }
            else if (CanWorkWithCombo(expected, numbers))
            {
                total += expected;
            }
        }

        Console.WriteLine(total);
    }

    private static bool CanWorkWithCombo(long expected, List<int> numbers)
    {
        var allCombos = GetPermutations(new List<string>
        {
            "Add",
            "Mul",
            "Com"
        }, numbers.Count - 1).Where(c => c.Any(i => i == "Com"));

        foreach (var combination in allCombos)
        {
            long total = numbers[0];
            for (var i = 1; i < numbers.Count; i++)
            {
                var number = numbers[i];
                if (combination.ElementAt(i - 1) == "Add")
                {
                    total += number;
                }
                else if (combination.ElementAt(i - 1) == "Mul")
                {
                    total *= number;
                }
                else if (combination.ElementAt(i - 1) == "Com")
                {
                    total = Convert.ToInt64($"{total}{number}");
                }
            }

            if (total == expected)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1)
        {
            return list.Select(t => new[] { t });
        }
        return GetPermutations(list, length - 1)
            .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private static bool CanWork(long expected, List<int> numbers)
    {
        var allCombos = GetPermutations(new List<string>
        {
            "Add",
            "Mul"
        }, numbers.Count - 1);

        foreach (var combination in allCombos)
        {
            long total = numbers[0];
            for (var i = 1; i < numbers.Count; i++)
            {
                var number = numbers[i];
                if (combination.ElementAt(i - 1) == "Add")
                {
                    total += number;
                }
                else if (combination.ElementAt(i - 1) == "Mul")
                {
                    total *= number;
                }
            }

            if (total == expected)
            {
                return true;
            }
        }

        return false;
    }
}