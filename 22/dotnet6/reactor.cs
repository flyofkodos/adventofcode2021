var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day22Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] lines)
{
    var cubes = new int[101, 101, 101]; // Array for the cubes
    foreach (var line in lines)
    {
        var operation = line.Split(' ')[0];
        var bit = operation == "on" ? 1 : 0;
        var temp = line.Split(' ')[^1].Split(',');
        var x = Array.ConvertAll(temp[0].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        var y = Array.ConvertAll(temp[1].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        var z = Array.ConvertAll(temp[2].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        // Loop for all X positions inside the window
        for (var cx = x[0]; cx <= x[1] && x[0] >= -50 && x[1] <= 50; cx++)
        {
            // Loop for all Y positions inside the window
            for (var cy = y[0]; cy <= y[1] && y[0] >= -50 && y[1] <= 50; cy++)
            {
                // Loop for all Z positions inside the window
                for (var cz = z[0]; cz <= z[1] && z[0] >= -50 && z[1] <= 50; cz++)
                {
                    cubes[cx + 50, cy + 50, cz + 50] = bit;
                }
            }
        }
    }

    return cubes.Cast<int>().Sum();
}

long Part2(string[] lines)
{
    // TODO
    return -1;
}