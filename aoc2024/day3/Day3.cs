using System.Text.RegularExpressions;

namespace aoc2024.day3;

public class Day3
{
    public static void SolvePart1()
    {
        var text = File.ReadAllText("./day3/input.txt");
        var matches = Regex.Matches(text, @"mul\((\d{1,3}),(\d{1,3})\)");
        
        Console.WriteLine(matches.Sum(m => Convert.ToInt32(m.Groups[1].Value)*Convert.ToInt32(m.Groups[2].Value)));
    }

    enum MatchingStack
    {
        None,
        Mul,
        Do,
        Dont
    }
    
    public static void SolvePart2()
    {
        var text = File.ReadAllText("./day3/input.txt");
        var muls = Regex.Matches(text, @"mul\((\d{1,3}),(\d{1,3})\)")
            .Select(r => new
        {
            r.Index,
            Groups = (GroupCollection?)r.Groups,
            Type = MatchingStack.Mul
        }).ToList();
        var enables = Regex.Matches(text, @"do\(\)")
            .Select(r => new
            {
                r.Index,
                Groups = (GroupCollection?)null,
                Type = MatchingStack.Do
            }).ToList();
        var disables = Regex.Matches(text, @"don't\(\)")
            .Select(r => new
            {
                r.Index,
                Groups = (GroupCollection?)null,
                Type = MatchingStack.Dont
            }).ToList();
        
        muls.AddRange(enables);
        muls.AddRange(disables);
        
        var ordered = muls.OrderBy(m => m.Index).ToList();

        var total = 0;
        var enabled = true;
        for (int i = 0; i < ordered.Count; i++)
        {
            switch (ordered[i].Type)
            {
                case MatchingStack.Mul:
                    if (enabled)
                    {
                        total += Convert.ToInt32(ordered[i].Groups[1].Value) *
                                 Convert.ToInt32(ordered[i].Groups[2].Value);
                    }
                    break;
                case MatchingStack.Do:
                    enabled = true;
                    break;
                case MatchingStack.Dont:
                    enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        Console.WriteLine(total);
    }
}