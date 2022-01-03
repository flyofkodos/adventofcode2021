using System.Reflection;

var path = Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day24Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");

string? Part1(string[] program)
{
    var counter = 0;
    var lowest = long.MaxValue;
    var result = Parallel.For(11111111111111, 99999999999999, (serial, loopState) =>
         {
             if (serial.ToString().Contains('0')) return;
             if (Interlocked.Increment(ref counter) == 10000)
             {
                 Console.Write($"Testing {serial} - {lowest}    \r");
                 counter = 0;
             }

             var result = Validate(ref program, serial.ToString());
             if (result == 0)
             {
                 Console.WriteLine($"Working serial {serial}");
                 result = serial;
                 loopState.Break();
             }
             if (lowest > result)
             {
                 Interlocked.Exchange(ref lowest, result);
             }
         }
       );
    return result.ToString();
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

long Part2(string[] lines)
{
    return -1L;
}
