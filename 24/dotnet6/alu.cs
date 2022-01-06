using System.Reflection;

var path = Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day24Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

long Part1(string[] program)
{
    // digit 1 = 7 to 9
    // digit 2 = 1
    // digit 3 = 1 to 2
    // digit 4 = 3 to 9
    // digit 5 = digit 4 - 2 (1 to 7)
    // digit 6 = 1 to 3
    // digit 7 = 5 to 9
    // digit 8 = digit 7 - 4 (1 to 5)
    // digit 9 = 9
    // digit 10 = 1
    // digit 11 = digit 6 + 6 (7 to 9)
    // digit 12 = digit 3 + 7 (8 to 9)
    // digit 13 = 9
    // digit 14 = digit 1 - 6 (1 to 3)
    var digits = new int[14];
    digits[0] = 9;
    digits[1] = 1;
    digits[2] = 2;
    digits[3] = 9;
    digits[4] = digits[3] - 2;
    digits[5] = 3;
    digits[6] = 9;
    digits[7] = digits[6] - 4;
    digits[8] = 9;
    digits[9] = 1;
    digits[10] = digits[5] + 6;
    digits[11] = digits[2] + 7;
    digits[12] = 9;
    digits[13] = digits[0] - 6;

    var serial = 0L;
    for (var d = 0; d < 14; d++)
    {
        serial *= 10;
        serial += digits[d];
    }
    // Sanity check
    if (0 == Validate(ref program, serial.ToString()))
        return serial;
    return -1;

}

long Validate(ref string[] lines, string model)
{
    var pointer = 0;
    var bits = new long[4];
    var operand2 = 0L;
    foreach (var instruction in lines)
    {
        var elements = instruction.Split(' ');
        if (elements.Length > 2 && !long.TryParse(elements[2], out operand2))
            operand2 = bits[elements[2][0] - 'w']; // Read the register value
        switch (elements[0])
        {
            case "inp":
                bits[elements[1][0] - 'w'] = (short)(model[pointer] - '0');
                Interlocked.Increment(ref pointer);
                break;
            case "add":
                bits[elements[1][0] - 'w'] += operand2;
                break;
            case "mul":
                bits[elements[1][0] - 'w'] *= operand2;
                break;
            case "div":
                bits[elements[1][0] - 'w'] /= operand2;
                break;
            case "mod":
                bits[elements[1][0] - 'w'] %= operand2;
                break;
            case "eql":
                bits[elements[1][0] - 'w'] = bits[elements[1][0] - 'w'] == operand2 ? 1 : 0;
                break;
        }
    }

    return bits[3];
}

long Part2(string[] program)
{
    // digit 1 = 7 to 9
    // digit 2 = 1
    // digit 3 = 1 to 2
    // digit 4 = 3 to 9
    // digit 5 = digit 4 - 2 (1 to 7)
    // digit 6 = 1 to 3
    // digit 7 = 5 to 9
    // digit 8 = digit 7 - 4 (1 to 5)
    // digit 9 = 9
    // digit 10 = 1
    // digit 11 = digit 6 + 6 (7 to 9)
    // digit 12 = digit 2 + 7 ( 8 to 9)
    // digit 13 = 9
    // digit 14 = digit 1 - 6 (1 to 3)
    var digits = new int[14];
    digits[0] = 7;
    digits[1] = 1;
    digits[2] = 1;
    digits[3] = 3;
    digits[4] = digits[3] - 2;
    digits[5] = 1;
    digits[6] = 5;
    digits[7] = digits[6] - 4;
    digits[8] = 9;
    digits[9] = 1;
    digits[10] = digits[5] + 6;
    digits[11] = digits[2] + 7;
    digits[12] = 9;
    digits[13] = digits[0] - 6;

    var serial = 0L;
    for (var d = 0; d < 14; d++)
    {
        serial *= 10;
        serial += digits[d];
    }
    // Sanity check
    if (0 == Validate(ref program, serial.ToString()))
        return serial;
    return -1;
}
