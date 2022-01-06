using System.Reflection;
using System.Collections.Concurrent;

var path = Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day24Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");

long Part1(string[] program)
{
    var counter = 0;
    var best = 0L;
    var lowest = long.MaxValue;
    var working = new ConcurrentBag<long>();
    var opts = new ParallelOptions();
    opts.MaxDegreeOfParallelism = Environment.ProcessorCount;
    System.Console.WriteLine($"Running on {opts.MaxDegreeOfParallelism} threads.");
    Parallel.For(11111111111111, 100000000000000, opts, serial =>
    // for (var serial = 99999999999999; serial >= 11111111111111; serial--)
    {
        if (serial.ToString().Contains('0')) return;
        if (Interlocked.Increment(ref counter) == 10000000)
        {
            Console.Write($"Testing {serial} - {lowest}    \r");
            counter = 0;
        }

        var result = Calculate(serial.ToString());
        if (result == 0)
        {
            Console.WriteLine($"\r\nWorking serial {serial}");
            working.Add(serial);
        }
        if (lowest == result && serial > best)
            best = result;
        if (lowest <= result) return;
        lowest = result;
        best = serial;
        if (Validate(ref program, serial.ToString()) != result)
        {
            Console.WriteLine($"Serial {serial} doesn't validate!!");
        }
    }
    );
    return working.Max();
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
long Calculate(string serial)
{
    var digits = serial.ToCharArray();
    var numbers = Array.ConvertAll(digits, c => c - '0');
    var z = ((numbers[0] * 26L + numbers[1] + 12) * 26 + numbers[2] + 14);
    if (numbers[4] != numbers[3] - 2)
    {
        z = z * 26 + numbers[4] + 3;
    }
    z = z * 26 + numbers[5] + 15;

    if (numbers[7] != numbers[6] - 4)
    {
        z = z * 26 + numbers[7] + 12;
    }
    if (numbers[9] != numbers[8] - 8)
    {
        z = z * 26 + numbers[9] + 12;
    }
    var x = z % 26;
    z /= 26;
    if (numbers[10] != x - 9)
    {
        z = z * 26 + numbers[11] + 3;
    }
    x = z % 26;
    z /= 26;
    if (numbers[11] != x - 7)
    {
        z = z * 26 + numbers[11] + 10;
    }
    x = z % 26;
    z /= 26;
    if (numbers[12] != x - 4)
    {
        z = z * 26 + numbers[12] + 14;
    }
    x = z % 26;
    z /= 26;
    if (numbers[13] != x - 6)
    {
        z = z * 26 + numbers[13] + 12;
    }
    return z;
}

long Part2(string[] lines)
{
    return -1L;
}
