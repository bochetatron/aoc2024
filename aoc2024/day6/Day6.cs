namespace aoc2024.day6;

public class Day6
{
    public static void SolvePart1()
    {
        var lines = File.ReadAllLines("./day6/input.txt");
        Height = lines.Length;
        Width = lines.Select(r => r.Length).Max();
        var map = new List<List<char>>();
        foreach (var line in lines)
        {
            map.Add(line.ToCharArray().ToList());
        }

        var i = map.FindIndex(row => row.Contains('^'));
        var j = map.First(row => row.Contains('^')).IndexOf('^');

        var escaped = false;
        var direction = 0;
        var visited = new HashSet<string>();
        visited.Add($"{i}-{j}");
        while (!escaped)
        {
            var result = NextMove(i, j, direction, map);

            escaped = result.escaped;
            if (!escaped)
            {
                visited.Add($"{result.i}-{result.j}");
                i = result.i;
                j = result.j;
                direction = result.direction;
            }
        }

        Console.WriteLine(visited.Count);
    }

    public static void SolvePart2()
    {
        var lines = File.ReadAllLines("./day6/input.txt");
        Height = lines.Length;
        Width = lines.Select(r => r.Length).Max();
        var map = new List<List<char>>();
        foreach (var line in lines)
        {
            map.Add(line.ToCharArray().ToList());
        }

        var i = map.FindIndex(row => row.Contains('^'));
        var j = map.First(row => row.Contains('^')).IndexOf('^');
        var loops = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int z = 0; z < Width; z++)
            {
                var copy = map.Select(l => new List<char>(l)).ToList();
                copy[y][z] = '#';
                if (y == i && j == z)
                {
                    continue;
                }

                if (HasLoop(i, j, 0, copy))
                {
                    loops++;
                }
            }
        }
        Console.WriteLine(loops);
    }

    private static bool HasLoop(int i, int j, int direction, List<List<char>> map)
    {
        var escaped = false;
        var looping = false;
        var visited = new HashSet<(int i, int j, int direction)>();
        visited.Add((i, j, direction));
        while (!escaped && !looping)
        {
            var result = NextMove(i, j, direction, map);
            if (!result.escaped)
            {
                if (!visited.Add((result.i, result.j, result.direction)))
                {
                    looping = true;
                }
            }
            escaped = result.escaped;
            if (!escaped)
            {
                i = result.i;
                j = result.j;
                direction = result.direction;
            }
        }
        return looping;
    }

    private static int Height = 0;
    private static int Width = 0;

    private static Dictionary<int, Dictionary<string, int>> DirectionOffset = new()
    {
        { 0, new() { { "x", -1 }, { "y", 0 } } },
        { 1, new() { { "x", 0 }, { "y", 1 } } },
        { 2, new() { { "x", 1 }, { "y", 0 } } },
        { 3, new() { { "x", 0 }, { "y", -1 } } }
    };

    private static Dictionary<int, int> NextDirection = new()
    {
        { 0, 1 },
        { 1, 2 },
        { 2, 3 },
        { 3, 0 }
    };

    private static (int i, int j, bool escaped, int direction) NextMove(int i, int j, int direction,
        List<List<char>> map)
    {
        var nextI = i + DirectionOffset[direction]["x"];
        var nextJ = j + DirectionOffset[direction]["y"];

        if (nextI < 0 || nextI >= Height || nextJ < 0 || nextJ >= Width)
        {
            return (nextI, nextJ, true, direction);
        }

        var moved = false;
        while (!moved)
        {
            if (CanMove(nextI, nextJ, map))
            {
                moved = true;
            }
            else
            {
                direction = NextDirection[direction];
                nextI = i + DirectionOffset[direction]["x"];
                nextJ = j + DirectionOffset[direction]["y"];
            }
        }

        return (nextI, nextJ, false, direction);
    }

    private static bool CanMove(int i, int j, List<List<char>> map)
    {
        return map[i][j] != '#';
    }
}