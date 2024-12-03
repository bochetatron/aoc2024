namespace aoc2024.day2;

public class Day2
{
    public static void SolvePart1()
    {
        var reports = File.ReadAllLines("./day2/input.txt");
        Console.WriteLine(reports.Count(r => IsValid(r.Split(" ").Select(i => Convert.ToInt32(i)).ToList())));
    }
    
    public static void SolvePart2()
    {
        var reports = File.ReadAllLines("./day2/input.txt");
        Console.WriteLine(reports.Count(r => IsValidSkipOne(r.Split(" ").Select(i => Convert.ToInt32(i)).ToList())));
    }

    private static bool IsValid(List<int> levels)
    {
        var valid = true;
        for (var i = 0; i < levels.Count - 1; i++)
        {
            if (Math.Abs(levels[i] - levels[i + 1]) > 3 || Math.Abs(levels[i] - levels[i + 1]) == 0)
            {
                valid = false;
                break;
            }
        }
        
        return valid && (levels.Order().SequenceEqual(levels) || levels.Order().Reverse().SequenceEqual(levels));
    }
    
    private static bool IsValidSkipOne(List<int> levels)
    {
        var valid = true;
        for (var i = 0; i < levels.Count - 1; i++)
        {
            if (Math.Abs(levels[i] - levels[i + 1]) > 3 || Math.Abs(levels[i] - levels[i + 1]) == 0)
            {
                valid = false;
                break;
            }
        }

        if (valid && (levels.Order().SequenceEqual(levels) || levels.Order().Reverse().SequenceEqual(levels)))
        {
            return true;
        }

        valid = false;
        for (var i = 0; i < levels.Count; i++)
        {
            var removedOne = new List<int>(levels);
            removedOne.RemoveAt(i);
            if (IsValid(removedOne))
            {
                valid = true;
                break;
            }
            
        }
        
        return valid;
    }
}