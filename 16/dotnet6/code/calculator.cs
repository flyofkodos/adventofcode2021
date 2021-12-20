using AdventOfCode;

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllText($"{path}\\Day16Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

long Part1(string input)
{
    // Convert the hex input to a binary string
    var binaryString = HexStringToBinaryString(input);
    // Parse the input into a PacketTree onject
    var tree = new PacketTree(binaryString);
    return tree.VersionTotal(); // Return the total of all packet versions
}

long Part2(string input)
{
    // TODO - Fix this as it is currently too low
    var binaryString = HexStringToBinaryString(input);
    var part2 = new PacketTree(binaryString);
    part2.Process();
    return part2.Calculation;
}

// Convert the provided hex string to an equivalent binary
string HexStringToBinaryString(string hexString)
{
    // Split to an array of integers
    var intChars = Convert.FromHexString(hexString).ToArray();
    // Convert this to binary strings
    var hexParts = intChars.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray();
    return string.Join("", hexParts); // Return the cincatenated binary strings
}