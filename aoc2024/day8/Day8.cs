namespace aoc2024.day8;

public class Day8
{
    public static void SolvePart1()
    {
        var lines = File.ReadAllLines("./day8/input.txt");
        var map = new Dictionary<char, List<(int x, int y)>>();
        Height = lines.Length;
        Width = lines.Select(l => l.Length).Max();
        
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var symbol = lines[i][j];
                if (char.IsNumber(symbol) || char.IsLetter(symbol))
                {
                    if (map.ContainsKey(symbol))
                    {
                        map[symbol].Add((i, j));
                    }
                    else
                    {
                        map.Add(symbol, [(i, j)]);
                    }
                }
            }
        }

        var allAntiNodes = new List<(int x, int y)>();
        foreach (var symbol in map.Keys)
        {
            allAntiNodes.AddRange(FindAntiNodes(map[symbol]));
        }
        
        Console.WriteLine(allAntiNodes.Distinct().Count());
    }
    
    public static void SolvePart2()
    {
        var lines = File.ReadAllLines("./day8/input.txt");
        var map = new Dictionary<char, List<(int x, int y)>>();
        Height = lines.Length;
        Width = lines.Select(l => l.Length).Max();
        
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var symbol = lines[i][j];
                if (char.IsNumber(symbol) || char.IsLetter(symbol))
                {
                    if (map.ContainsKey(symbol))
                    {
                        map[symbol].Add((i, j));
                    }
                    else
                    {
                        map.Add(symbol, [(i, j)]);
                    }
                }
            }
        }

        var allAntiNodes = new List<(int x, int y)>();
        foreach (var symbol in map.Keys)
        {
            allAntiNodes.AddRange(FindAntiNodesFullLine(map[symbol]));
        }
        
        Console.WriteLine(allAntiNodes.Distinct().Count());
    }

    private static int Height = 0;
    private static int Width = 0;

    private static List<(int x, int y)> FindAntiNodes(List<(int x, int y)> postitions)
    {
        var antiNodes = new List<(int x, int y)>();
        for (var i = 0; i < postitions.Count; i++)
        {
            var posA = postitions[i];
            for (var j = postitions.Count - 1; j > -1; j--)
            {
                var posB = postitions[j];
                if (posA == posB)
                {
                    continue;
                }

                var antiPos = AntiPos(posA, posB);
                if (antiPos is { x: >= 0, y: >= 0 } && antiPos.x < Height && antiPos.y < Width )
                {
                    antiNodes.Add(antiPos);
                }
            }
        }

        return antiNodes;
    }
    
    private static List<(int x, int y)> FindAntiNodesFullLine(List<(int x, int y)> postitions)
    {
        var antiNodes = new List<(int x, int y)>();
        for (var i = 0; i < postitions.Count; i++)
        {
            var posA = postitions[i];
            for (var j = postitions.Count - 1; j > -1; j--)
            {
                var posB = postitions[j];
                if (posA == posB)
                {
                    continue;
                }

                var calcA = posA;
                var calcB = posB;
                antiNodes.Add(posA);
                antiNodes.Add(posB);
                var antiPos = AntiPos(calcA, calcB);
                while (antiPos is { x: >= 0, y: >= 0 } && antiPos.x < Height && antiPos.y < Width)
                {
                    antiNodes.Add(antiPos);
                    calcA = calcB;
                    calcB = antiPos;
                    antiPos = AntiPos(calcA, calcB);
                }
            }
        }

        return antiNodes;
    }

    private static (int x, int y) AntiPos((int x, int y) posA, (int x, int y) posB)
    {
        return (
            posB.x + (posB.x - posA.x),
            posB.y + (posB.y - posA.y)
            );
    }
}