namespace aoc2024.day4;

public class Day4
{
    public static void SolvePart1()
    {
        var rows = File.ReadAllLines("./day4/input.txt");
        Height = rows.Length;
        Width = rows.Select(r => r.Length).Max();
        var matrix = rows.Select(row => row.ToCharArray().ToList()).ToList();
        var count = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                var letter = matrix[i][j];
                if (letter == 'X')
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (SearchInDirection(i, j, matrix, 'M', k))
                        {
                            count++;
                        }
                    }
                }
            }
        }
        Console.WriteLine(count);
    }
    
    public static void SolvePart2()
    {
        var rows = File.ReadAllLines("./day4/input.txt");
        Height = rows.Length;
        Width = rows.Select(r => r.Length).Max();
        var matrix = rows.Select(row => row.ToCharArray().ToList()).ToList();
        var count = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                var letter = matrix[i][j];
                if (letter == 'A')
                {
                    if (
                            (SearchInDirectionStep(i, j, matrix, 'M', 6)
                            && SearchInDirectionStep(i, j, matrix, 'S', 2)
                            || SearchInDirectionStep(i, j, matrix, 'S', 6)
                            && SearchInDirectionStep(i, j, matrix, 'M', 2)) 
                        &&
                            (SearchInDirectionStep(i, j, matrix, 'M', 0)
                             && SearchInDirectionStep(i, j, matrix, 'S', 8)
                             || SearchInDirectionStep(i, j, matrix, 'S', 0)
                             && SearchInDirectionStep(i, j, matrix, 'M', 8))
                    )
                    {
                        count++;
                    }
                }
            }
        }
        Console.WriteLine(count);
    }

    private static int Height = 0;
    private static int Width = 0;

    private static Dictionary<char, char> NextMatch = new()
    {
        {'X', 'M'},
        {'M', 'A'},
        {'A', 'S'}
    };

    private static Dictionary<int, Dictionary<string, int>> DirectionOffset = new()
    {
        {0, new(){ {"x", -1}, {"y", -1} }},
        {1, new(){ {"x", -1}, {"y", 0} }},
        {2, new(){ {"x", -1}, {"y", 1} }},
        {3, new(){ {"x", 0}, {"y", -1} }},
        {4, new(){ {"x", 0}, {"y", 0} }},
        {5, new(){ {"x", 0}, {"y", 1} }},
        {6, new(){ {"x", 1}, {"y", -1} }},
        {7, new(){ {"x", 1}, {"y", 0} }},
        {8, new(){ {"x", 1}, {"y", 1} }}
    };

    private static bool SearchInDirectionStep(int i, int j, List<List<char>> matrix, char match, int direction)
    {
        var x = i + DirectionOffset[direction]["x"];
        var y = j + DirectionOffset[direction]["y"];

        if (x < 0 || x >= Height || y < 0 || y >= Width)
        {
            return false;
        }

        return matrix[x][y] == match;
    }

    private static bool SearchInDirection(int i, int j, List<List<char>> matrix, char match, int direction)
    {
        var x = i + DirectionOffset[direction]["x"];
        var y = j + DirectionOffset[direction]["y"];
        
        if (x < 0 || x >= Height || y < 0 || y >= Width)
        {
            return false;
        }

        if (matrix[x][y] != match)
        {
            return false;
        }

        var hasNextMatch = NextMatch.TryGetValue(match, out var next);
        return (!hasNextMatch && match == 'S') || SearchInDirection(x, y, matrix, next, direction);
    }
}