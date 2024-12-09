using System.Collections.Specialized;

namespace aoc2024.day9;

public class Day9
{
    public static void SolvePart1()
    {
        var data = File.ReadAllText("./day9/input.txt");

        var layout = new List<string>();
        var fileId = 0;
        for (var i = 0; i < data.Length; i++)
        {
            if (i % 2 == 0)
            {
                layout.AddRange(Enumerable.Repeat(fileId.ToString(), Convert.ToInt32(data[i].ToString())));
                fileId++;
            }
            else
            {
                layout.AddRange(Enumerable.Repeat(".", Convert.ToInt32(data[i].ToString())));
            }
        }
        
        for (var i = layout.Count - 1; i > -1; i--)
        {
            var toMove = layout[i];
            if (toMove == ".")
            {
                continue;
            }

            var firstSpace = layout.IndexOf(".");
            if (firstSpace > i)
            {
                break;
            }
            layout.RemoveAt(firstSpace);
            layout.Insert(firstSpace, toMove);
            layout.RemoveAt(i);
            layout.Add(".");
        }

        var hash = CreateHash(layout);
        
        Console.WriteLine(hash);
    }
    
    public static void SolvePart2()
    {
        var data = File.ReadAllText("./day9/input.txt");

        var layout = new List<string>();
        var fileId = 0;
        for (var i = 0; i < data.Length; i++)
        {
            if (i % 2 == 0)
            {
                layout.AddRange(Enumerable.Repeat(fileId.ToString(), Convert.ToInt32(data[i].ToString())));
                fileId++;
            }
            else
            {
                layout.AddRange(Enumerable.Repeat(".", Convert.ToInt32(data[i].ToString())));
            }
        }
        
        for (var i = layout.Count - 1; i > -1; i--)
        {
            var toMove = layout[i];
            if (toMove == ".")
            {
                continue;
            }

            var freeSpaces = FindAllSpacesBeforeBlock(layout, i);
            if (freeSpaces.Count == 0)
            {
                break;
            }
            
            var blockInfo = SizeOfBlock(toMove, layout);
            var availableFreeSpace = freeSpaces.Where(s => s.length >= blockInfo.size);

            if (availableFreeSpace.Any())
            {
                SwapBlock(availableFreeSpace.First().start, (blockStart: blockInfo.startIndex, blockEnd: i), toMove, ref layout);
            }
        }

        var hash = CreateHash(layout);
        
        Console.WriteLine(hash);
    }

    private static List<(int start, int length)> FindAllSpacesBeforeBlock(List<string> layout, int upTo)
    {
        var freeSpaces = new List<(int start, int length)>();
        var inSpace = false;
        var currentStart = 0;
        var currentLength = 0;
        for (var i = 0; i < upTo; i++)
        {
            if (layout[i] == ".")
            {
                if (!inSpace)
                {
                    inSpace = true;
                    currentStart = i;
                    currentLength++;
                }
                else
                {
                    currentLength++;
                }
            }
            else
            {
                freeSpaces.Add((currentStart, currentLength));
                inSpace = false;
                currentLength = 0;
                currentStart = 0;
            }
        }

        return freeSpaces;
    }

    private static void SwapBlock(int firstSpace, (int blockStart, int blockEnd) blockInfo, string block, ref List<string> layout)
    {
        var len = blockInfo.blockEnd - blockInfo.blockStart + 1;
        for (var i = firstSpace; i < firstSpace + len; i++)
        {
            layout[i] = block;
        }

        for (int i = blockInfo.blockStart; i <= blockInfo.blockEnd; i++)
        {
            layout[i] = ".";
        }
    }

    private static (int size, int startIndex) SizeOfBlock(string blockName, List<string> layout)
    {
        var size = layout.LastIndexOf(blockName) - layout.IndexOf(blockName) + 1;
        return (size, layout.IndexOf(blockName));
    }

    private static long CreateHash(List<string> layout)
    {
        var hash = 0L;
        for (var i = 0; i < layout.Count; i++)
        {
            var item = layout[i];
            if (item == ".")
            {
                continue;
            }

            hash += i*Convert.ToInt32(item);
        }

        return hash;
    }
}