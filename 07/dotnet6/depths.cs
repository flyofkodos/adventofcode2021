var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var real = File.ReadAllText($"{path}\\Day07Input.txt");
var depths = Array.ConvertAll(real.Split(','), int.Parse).ToList();
Console.WriteLine($"Part 1 takes {Part1(depths)} fuel");
Console.WriteLine($"Part 2 takes {Part2(depths)} fuel");

int Part1(List<int> depths)
{
    // Work out the median for the provided depths
    int optimalDepth;
    depths.Sort();
    if (depths.Count % 1 == 1)
    {
        optimalDepth = (depths[(int)Math.Ceiling(depths.Count / 2.0)] + depths[(int)Math.Floor(depths.Count / 2.0)]) / 2;
    }
    else
    {
        optimalDepth = depths[depths.Count / 2];
    }
    // Return the sum of the differences from the optimal depth
    return depths.Sum(depth => Math.Abs(depth - optimalDepth));
}

int Part2(List<int> depths)
{
    List<int> fuelUsed = new();
    depths.Sort();
    for (var test = depths[0]; test <= depths[^1]; test++) // Loop through every possible depth
    {
        // Fuel used is a triangular number so add the sum of (x * x+1) / 2 to the list
        fuelUsed.Add(depths.Sum(depth => (Math.Abs(depth - test) * (1 + Math.Abs(depth - test))) / 2));
    }
    fuelUsed.Sort(); // Sort the fuel usage list
    return fuelUsed[0]; // and return the smallest
}