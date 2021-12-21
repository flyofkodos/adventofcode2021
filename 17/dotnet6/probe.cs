var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllText($"{path}\\Day17Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string lines)
{
    var temp = lines.Split(':')[1].Split(',');
    var ystuff = temp[1].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
    var bottom = int.Parse(ystuff[0]);
    var dy = (-bottom) - 1; // Highest value to end at the bottom of the target area
    return (dy * (dy + 1) / 2); // Return the sum of the positive vertical velocities
}

int Part2(string input)
{
    // TODO
    return -1;
}
