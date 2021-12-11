var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day11Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

long Part1(string[] lines)
{
    var total = 0L;
    var width = lines[0].Length;
    var height = lines.Length;
    var octopus = new int[width, height];
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            octopus[x, y] = int.Parse(lines[y][x].ToString());
        }
    }

    for (var loop = 0; loop < 100; loop++)
    {
        List<(int, int)> stack = new();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                octopus[x, y]++;
                if (octopus[x, y] <= 9) continue;
                stack.Add((x, y));
            }
        }

        while (stack.Count > 0)
        {
            var nx = stack[0].Item1;
            var ny = stack[0].Item2;
            if (octopus[nx, ny] == 0) // Check this hasn't already flashed
            {
                stack.RemoveAt(0);
                continue;
            }

            total++;
            octopus[nx, ny] = 0;
            if (nx > 0 && octopus[nx - 1, ny] > 0)
            {
                octopus[nx - 1, ny]++;
                if (octopus[nx - 1, ny] > 9)
                {
                    stack.Add((nx - 1, ny));
                }
            }

            if (nx > 0 && ny > 0 && octopus[nx - 1, ny - 1] > 0)
            {
                octopus[nx - 1, ny - 1]++;
                if (octopus[nx - 1, ny - 1] > 9)
                {
                    stack.Add((nx - 1, ny - 1));
                }
            }

            if (nx > 0 && ny < width - 1 && octopus[nx - 1, ny + 1] > 0)
            {
                octopus[nx - 1, ny + 1]++;
                if (octopus[nx - 1, ny + 1] > 9)
                {
                    stack.Add((nx - 1, ny + 1));
                }
            }

            if (nx < width - 1 && octopus[nx + 1, ny] > 0)
            {
                octopus[nx + 1, ny]++;
                if (octopus[nx + 1, ny] > 9)
                {
                    stack.Add((nx + 1, ny));
                }
            }

            if (nx < width - 1 && ny > 0 && octopus[nx + 1, ny - 1] > 0)
            {
                octopus[nx + 1, ny - 1]++;
                if (octopus[nx + 1, ny - 1] > 9)
                {
                    stack.Add((nx + 1, ny - 1));
                }
            }

            if (nx < width - 1 && ny < height - 1 && octopus[nx + 1, ny + 1] > 0)
            {
                octopus[nx + 1, ny + 1]++;
                if (octopus[nx + 1, ny + 1] > 9)
                {
                    stack.Add((nx + 1, ny + 1));
                }
            }

            if (ny > 0 && octopus[nx, ny - 1] > 0)
            {
                octopus[nx, ny - 1]++;
                if (octopus[nx, ny - 1] > 9)
                {
                    stack.Add((nx, ny - 1));
                }
            }

            if (ny < height - 1 && octopus[nx, ny + 1] > 0)
            {
                octopus[nx, ny + 1]++;
                if (octopus[nx, ny + 1] > 9)
                {
                    stack.Add((nx, ny + 1));
                }
            }

            stack.RemoveAt(0);
        }
    }
    return total;
}

long Part2(string[] lines)
{

    var width = lines[0].Length;
    var height = lines.Length;
    var octopus = new int[width, height];
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            octopus[x, y] = int.Parse(lines[y][x].ToString());
        }
    }

    var loop = 0L;

    while (octopus.Cast<int>().Sum() > 0)
    {
        loop++;

        List<(int, int)> stack = new();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                octopus[x, y]++;
                if (octopus[x, y] <= 9) continue;
                stack.Add((x, y));
            }
        }

        while (stack.Count > 0)
        {
            var nx = stack[0].Item1;
            var ny = stack[0].Item2;
            if (octopus[nx, ny] == 0) // Check this hasn't already flashed
            {
                stack.RemoveAt(0);
                continue;
            }

            octopus[nx, ny] = 0;
            if (nx > 0 && octopus[nx - 1, ny] > 0)
            {
                octopus[nx - 1, ny]++;
                if (octopus[nx - 1, ny] > 9)
                {
                    stack.Add((nx - 1, ny));
                }
            }

            if (nx > 0 && ny > 0 && octopus[nx - 1, ny - 1] > 0)
            {
                octopus[nx - 1, ny - 1]++;
                if (octopus[nx - 1, ny - 1] > 9)
                {
                    stack.Add((nx - 1, ny - 1));
                }
            }

            if (nx > 0 && ny < width - 1 && octopus[nx - 1, ny + 1] > 0)
            {
                octopus[nx - 1, ny + 1]++;
                if (octopus[nx - 1, ny + 1] > 9)
                {
                    stack.Add((nx - 1, ny + 1));
                }
            }

            if (nx < width - 1 && octopus[nx + 1, ny] > 0)
            {
                octopus[nx + 1, ny]++;
                if (octopus[nx + 1, ny] > 9)
                {
                    stack.Add((nx + 1, ny));
                }
            }

            if (nx < width - 1 && ny > 0 && octopus[nx + 1, ny - 1] > 0)
            {
                octopus[nx + 1, ny - 1]++;
                if (octopus[nx + 1, ny - 1] > 9)
                {
                    stack.Add((nx + 1, ny - 1));
                }
            }

            if (nx < width - 1 && ny < height - 1 && octopus[nx + 1, ny + 1] > 0)
            {
                octopus[nx + 1, ny + 1]++;
                if (octopus[nx + 1, ny + 1] > 9)
                {
                    stack.Add((nx + 1, ny + 1));
                }
            }

            if (ny > 0 && octopus[nx, ny - 1] > 0)
            {
                octopus[nx, ny - 1]++;
                if (octopus[nx, ny - 1] > 9)
                {
                    stack.Add((nx, ny - 1));
                }
            }

            if (ny < height - 1 && octopus[nx, ny + 1] > 0)
            {
                octopus[nx, ny + 1]++;
                if (octopus[nx, ny + 1] > 9)
                {
                    stack.Add((nx, ny + 1));
                }
            }

            stack.RemoveAt(0);
        }
    }
    return loop;
}