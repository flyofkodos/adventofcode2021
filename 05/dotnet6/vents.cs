string[] Test;
string[] Real;
int[,] Grid = new int[1000, 1000];

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
Test = File.ReadAllLines($"{path}\\Day05Test.txt");
Real = File.ReadAllLines($"{path}\\Day05Input.txt");
Console.WriteLine($"Test overlaps = {Process(Test)}");
// Draw the test result to compare with the example
for (var y = 0; y < 10; y++)
{
    for (var x = 0; x < 10; x++)
    {
        Console.Write(Grid[x, y]);
    }
    Console.WriteLine();
}
Console.WriteLine($"Part1 overlaps = {Process(Real)}");
Console.WriteLine($"Part2 test = {Process(Test, true)}");
// Draw the test result to compare with the example
for (var y = 0; y < 10; y++)
{
    for (var x = 0; x < 10; x++)
    {
        Console.Write(Grid[x, y]);
    }
    Console.WriteLine();
}
Console.WriteLine($"Part2 overlaps = {Process(Real, true)}");

void SetVars()
{
    for (var x = 0; x < 1000; x++)
    {
        for (var y = 0; y < 1000; y++)
        {
            Grid[x, y] = 0;
        }
    }
}

int Process(string[] input, bool diagonals = false)
{
    SetVars();
    foreach (var vent in input)
    {
        var parts = vent.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var start = Array.ConvertAll(parts[0].Split(','), int.Parse);
        var end = Array.ConvertAll(parts[2].Split(','), int.Parse);
        if (start[0] != end[0] && start[1] == end[1]) // Horizontal vent
        {
            // Make sure we're going from left to right
            for (var x = Math.Min(start[0], end[0]); x <= Math.Max(start[0], end[0]); x++)
            {
                Grid[x, Convert.ToInt32(start[1])]++; // Increment the grid location
            }
        }
        else
        {
            if (start[0] == end[0] && start[1] != end[1])
            {
                // Make sure we're going from top to bottom
                for (var y = Math.Min(start[1], end[1]); y <= Math.Max(start[1], end[1]); y++)
                {
                    Grid[Convert.ToInt32(start[0]), y]++; // Increment the grid location
                }
            }
            else
            {
                // Check for 45 degree lines if the diagonals flag was passed
                if (diagonals && Math.Abs(end[0] - start[0]) == Math.Abs(end[1] - start[1]))
                {
                    var dx = end[0] > start[0] ? 1 : -1; // Calculate x direction (-1 for left, 1 for right)
                    var dy = end[1] > start[1] ? 1 : -1; // Calculate y direction (-1 for up, 1 for down)
                    var x = start[0];
                    var y = start[1];
                    while (x != end[0] + dx) // Loop until x goes beyond the end point
                    {
                        Grid[x, y]++; // Increment the grid location
                        x += dx; // Calculate the x position of the next vent section
                        y += dy; // Calculate the y position of the next vent section
                    }
                }
            }
        }
    }
    return Grid.Cast<int>().Count(i => i > 1); // Return the count of all elements > 1
}