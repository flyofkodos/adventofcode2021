var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day09Input.txt").ToArray();
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] lines)
{
    var xMax = lines[0].Length;
    var yMax = lines.Length;
    var total = 0;
    var surrounds = new int[4]; // Array for the surrounding points
    for (var y = 0; y < yMax; y++)
    {
        for (var x = 0; x < xMax; x++) // Loop through each point
        {
            var height = int.Parse(lines[y][x].ToString());
            surrounds[0] = x > 0 ? int.Parse((lines[y][x - 1]).ToString()) : 10;
            surrounds[1] = x < xMax - 1 ? int.Parse((lines[y][x + 1]).ToString()) : 10;
            surrounds[2] = y > 0 ? int.Parse((lines[y - 1][x]).ToString()) : 10;
            surrounds[3] = y < yMax - 1 ? int.Parse((lines[y + 1][x]).ToString()) : 10;
            if (height < surrounds.Min()) // Is this the lowest local point?
            {
                total += height + 1;
            }
        }
    }
    return total;
}

int Part2(string[] lines)
{
    var xMax = lines[0].Length;
    var yMax = lines.Length;
    var lowPoints = new List<(int, int)>();
    var surrounds = new int[4];
    for (var y = 0; y < yMax; y++)
    {
        for (var x = 0; x < xMax; x++)
        {
            var height = int.Parse(lines[y][x].ToString());
            // Compare each point around us (use 10 when we're at the edge)
            surrounds[0] = x > 0 ? int.Parse((lines[y][x - 1]).ToString()) : 10;
            surrounds[1] = x < xMax - 1 ? int.Parse((lines[y][x + 1]).ToString()) : 10;
            surrounds[2] = y > 0 ? int.Parse((lines[y - 1][x]).ToString()) : 10;
            surrounds[3] = y < yMax - 1 ? int.Parse((lines[y + 1][x]).ToString()) : 10;
            if (height < surrounds.Min()) // Are we the lowest point?
            {
                // Add the lowest point co-ordinates to the list for later
                lowPoints.Add(new ValueTuple<int, int>(x, y));
            }
        }
    }

    // Get a list of all basin sizes
    var basins = lowPoints.Select(basin => basinSize(lines, basin)).ToList();
    basins.Sort(); // Sort the basin sizes

    return basins[^1] * basins[^2] * basins[^3];  // Multiply the largest 3 basins
}

int basinSize(string[] array, ValueTuple<int, int> lowPoint)
{
    var right = array[0].Length; // Get array width
    var bottom = array.Length; // and height
    var plots = new int[right, bottom]; // and create an 2D array based on them

    // convert the string array to a grid of ints
    for (var arr = 0; arr < bottom; arr++)
    {
        for (var a = 0; a < right; a++)
        {
            plots[a, arr] = int.Parse(array[arr][a].ToString());
        }
    }

    // Adapted from the fill routine on page 85 of the book
    // Graphics Programming on Your Electron
    // by Jim McGregor and Alan Watt
    // ISBN 0552991023
    var size = 0; // Reset the basin size
    var stack = new List<ValueTuple<int, int>>
            {
                lowPoint // Seed the stack with the basin low point
            };
    while (stack.Count > 0) // Loop until the stack is empty
    {
        var x = stack[0].Item1; // Get x from the bottom of the stack
        var y = stack[0].Item2; // Get y from the bottom of the stack

        if (plots[x, y] < 9) // Double-check we haven't hit a boundary
        {
            size++; // Increment the size counter
            plots[x, y] = 10; // Make sure this point doesn't get counted again
            if (x > 0 && plots[x - 1, y] < 9)
            {
                stack.Add((x - 1, y)); // Stack the point to the left if it's valid
            }

            if (x < right - 1 && plots[x + 1, y] < 9)
            {
                stack.Add((x + 1, y)); // Stack the point to the right if it's valid
            }

            if (y > 0 && plots[x, y - 1] < 9)
            {
                stack.Add((x, y - 1)); // Stack the point to the top if it's valid
            }

            if (y < bottom - 1 && plots[x, y + 1] < 9)
            {
                stack.Add((x, y + 1)); // Stack the point to the bottom if it's valid
            }
        }
        stack.Remove((x, y)); // Remove the current point from the stack
    }
    return size; // Return the basin size
}
