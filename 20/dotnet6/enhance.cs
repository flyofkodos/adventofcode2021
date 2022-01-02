using System.Collections.Concurrent;

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day20Input.txt");
Console.WriteLine($"Part 1 is {Part1(input, 2)}");
Console.WriteLine($"Part 2 is {Part1(input, 50)}");

int Part1(string[] lines, int Passes)
{
    var algorithm = lines[0];
    var pixels = new List<(int x, int y)>();
    for (var y = 2; y < lines.Length; y++)
    {
        var tempLine = lines[y].ToCharArray();
        for (var x = 0; x < tempLine.Length; x++)
        {
            if (tempLine[x] == '#')
            {
                pixels.Add((x, y - 2));
            }
        }
    }

    var newPixels = new ConcurrentBag<(int x, int y)>();
    for (var loop = 0; loop < Passes; loop++)
    {
        var startx = -(Passes * 2) + loop;
        var endx = lines[^1].Length + Passes * 2 - loop;
        var starty = -(Passes * 2) + loop;
        var endy = lines.Length + Passes * 2 - loop;
        for (var px = startx; px <= endx; px++)
            Parallel.For(starty, endy + 1, py =>
            {
                var square = pixels.Where(t =>
                    t.x >= px - 1 && t.x <= px + 1 && t.y >= py - 1 && t.y <= py + 1).ToList();
                var bin = 0;

                Parallel.ForEach(square, sq =>
                {
                    var (x, y) = sq;
                    Interlocked.Add(ref bin, 1 << ((py - y + 1) * 3 + px - x + 1));
                });
                if (algorithm[bin] == '#') newPixels.Add((px, py));
            });
        pixels.Clear();
        pixels.AddRange(newPixels);
        newPixels.Clear();
    }
    return pixels.Count;
}
