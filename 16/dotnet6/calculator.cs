int _versionTotal; // Var to track the version total.

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllText($"{path}\\Day16Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string input)
{
    var binaryString = HexStringToBinaryString(input); // Convert the input to a binary string
    _versionTotal = 0; // Make sure the total is cleared
    while (binaryString.Length > 6) // Loop while there's at least a header left to process
    {
        binaryString = ParsePacket(binaryString);
    }
    return _versionTotal;
}

int Part2(string input)
{
    // TODO
    return -1;
}

string ParsePacket(string incoming)
{
    var version = Convert.ToInt16(incoming[..3], 2); // 1st 3 bits are the version
    _versionTotal += version;
    var typeId = Convert.ToInt16(incoming[3..6], 2); // Next 3 bits are the type
    incoming = incoming[6..]; // Remove the header bits
    switch (typeId)
    {
        case 4: // Literal value packet
            while (incoming.Length > 5 && incoming[0] == '1') // Flag for more values to come
            {
                incoming = incoming[5..]; // Just skip the value for part 1
            }
            incoming = incoming[5..]; // Read the tail value

            break;
        default: // Everything else is an Operator packet
            incoming = ParseOperator(incoming); // Parse the operator packet
            break;
    }
    return incoming;
}

string ParseOperator(string incoming)
{
    if (incoming[0] == '0') // Bit length
    {
        incoming = incoming[16..];
        incoming = ParsePacket(incoming);
    }
    else // Subpackets
    {
        var subPackets = Convert.ToInt16(incoming[1..12], 2);
        incoming = incoming[12..];
        for (var p = 1; p <= subPackets; p++)
        {
            incoming = ParsePacket(incoming);
        }
    }
    return incoming;
}

string HexStringToBinaryString(string hexString)
{
    // Convert the provided hex string to an equivalent binary
    var intChars = Convert.FromHexString(hexString).ToArray(); // Split to an array of integers
    var hexParts = intChars.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray(); // Convert this to binary strings
    return string.Join("", hexParts); // Return the cincatenated binary strings
}