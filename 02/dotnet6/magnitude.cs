var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
string[] command = System.IO.File.ReadAllLines($"{Path.GetDirectoryName(path)}\\input.txt");

// Part 1
int horiz = 0, depth = 0;
foreach (string c in command)
{
    var parts = c.Split(' ');
    int magnitude = int.Parse(parts[1]);
    switch (parts[0])
    {
        case "forward":
            horiz += magnitude;
            break;
        case "backward":
            horiz -= magnitude;
            break;
        case "down":
            depth += magnitude;
            break;
        case "up":
            depth -= magnitude;
            break;
    }
}
Console.WriteLine($"Depth {depth} pos {horiz} - {horiz * depth}");

// Part 2
horiz = 0;
depth = 0;
int aim = 0;
foreach (string c in command)
{
    var parts = c.Split(' ');
    int magnitude = int.Parse(parts[1]);
    switch (parts[0])
    {
        case "forward":
            horiz += magnitude;
            depth += (aim * magnitude);
            break;
        case "backward":
            horiz -= magnitude;
            depth -= (aim * magnitude);
            break;
        case "down":
            aim += magnitude;
            break;
        case "up":
            aim -= magnitude;
            break;
    }
}
Console.WriteLine($"Depth {depth} pos {horiz} - {horiz * depth}");
