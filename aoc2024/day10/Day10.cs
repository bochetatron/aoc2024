namespace aoc2024.day10;

public class Day10
{
    public static void SolvePart1()
    {
        var map = File
            .ReadAllLines("./day10/input.txt")
            .Select(
                r => r.ToCharArray()
                .Select(c => c.ToString())
                .ToList()
            )
            .ToList();
        
        Height = map.Count;
        Width = map.Select(l => l.Count).Max();

        var trailheadScore = new Dictionary<(int i, int j), int>();
        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == "0")
                {
                    trailheadScore.Add((i, j), CalculateTrail((i, j), map).Distinct().Count());
                }
            }
        }
        Console.WriteLine(trailheadScore.Sum(k => k.Value));
    }
    
    public static void SolvePart2()
    {
        var map = File
            .ReadAllLines("./day10/input.txt")
            .Select(
                r => r.ToCharArray()
                    .Select(c => c.ToString())
                    .ToList()
            )
            .ToList();
        
        Height = map.Count;
        Width = map.Select(l => l.Count).Max();

        var trailheadRating = new Dictionary<(int i, int j), int>();
        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == "0")
                {
                    trailheadRating.Add((i, j), CalculateTrail((i, j), map).Count);
                }
            }
        }
        Console.WriteLine(trailheadRating.Sum(k => k.Value));
    }

    private static int Height = 0;
    private static int Width = 0;

    private static List<(int i, int j)> CalculateTrail((int i, int j) start, List<List<string>> map)
    {
        var found = new List<(int i, int j)>();
        var nextTarget = (Convert.ToInt32(map[start.i][start.j]) + 1).ToString();
        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                if (i < 0 && j != 0 || i > 0 && j != 0)
                {
                    continue;
                }
                if (j < 0 && i != 0 || j > 0 && i != 0)
                {
                    continue;
                }
                var calcI = start.i + i;
                var calcJ = start.j + j;
                if (calcI < 0 || calcJ < 0 || calcI >= Height || calcJ >= Width || map[calcI][calcJ] != nextTarget)
                {
                    continue;
                }

                if (nextTarget == "9")
                {
                    found.Add((calcI, calcJ));
                }
                else
                {
                    found.AddRange(CalculateTrail((calcI, calcJ), map));
                }
            }
        }

        return found;
    }
}