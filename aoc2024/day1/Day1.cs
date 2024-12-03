namespace aoc2024.day1;

public class Day1
{
    public static void SolvePart1()
    {
        var lines = File.ReadAllLines("./day1/input.txt");
        var sets = lines
            .SelectMany(r => r.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select((r, i) =>
                new
                {
                    Index = i,
                    Val = Convert.ToInt32(r)
                })
            .GroupBy(i => i.Index % 2 == 0)
            .Select(i => i.Select(v => v.Val))
            .ToList();
        var first = sets[0].Order().ToList();
        var second = sets[1].Order().ToList();
        double totalDiff = 0;
        for (var i = 0; i < first.Count; i++)
        {
            totalDiff += Math.Abs(second[i] - first[i]);
        }
        Console.WriteLine(totalDiff);
    }
    
    public static void SolvePart2()
    {
        var lines = File.ReadAllLines("./day1/input.txt");
        var sets = lines
            .SelectMany(r => r.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select((r, i) =>
                new
                {
                    Index = i,
                    Val = Convert.ToInt32(r)
                })
            .GroupBy(i => i.Index % 2 == 0)
            .Select(i => i.Select(v => v.Val))
            .ToList();
        var first = sets[0].Order().ToList();
        var second = sets[1].Order().ToList();
        double totalScore = 0;
        for (var i = 0; i < first.Count; i++)
        {
            totalScore += second.Count(s=> s == first[i]) * first[i];
        }
        Console.WriteLine(totalScore);
    }
}