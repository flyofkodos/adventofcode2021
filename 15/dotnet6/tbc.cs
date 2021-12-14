var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day15Input.txt");
Console.WriteLine($"Part 1 is {Part1(input, 10)}");
Console.WriteLine($"Part 1 is {Part1(input, 40)}");

// TODO - code when the puzzle is released