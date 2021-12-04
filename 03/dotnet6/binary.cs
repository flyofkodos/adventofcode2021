int[] Test = { 0b00100, 0b11110, 0b10110, 0b10111, 0b10101, 0b01111, 0b00111, 0b11100, 0b10000, 0b11001, 0b00010, 0b01010 };

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
var temp = System.IO.File.ReadAllLines($"{Path.GetDirectoryName(path)}\\input.txt");
var Real = (from x in temp
            select Convert.ToInt32(x, 2)).ToArray();
Console.WriteLine($"test = {Day3(Test)}");
Console.WriteLine($"part1 = {Day3(Real)}");
Console.WriteLine($"part2 test = {Day3Pt2(Test)}");
Console.WriteLine($"part2 = {Day3Pt2(Real)}");

static int Day3(int[] readings)
{
    var bits = Convert.ToString(readings.Max(), 2).Length - 1;
    int gamma = 0, epsilon = 0;
    for (var x = bits; x >= 0; x--)
    {
        var bit = 1 << x;
        int ones = 0, zeros = 0;
        foreach (var y in readings)
        {
            if ((y & bit) > 0)
            {
                ones++;
            }
            else
            {
                zeros++;
            }
        }
        gamma += ones > zeros ? bit : 0;
        epsilon += ones > zeros ? 0 : bit;
    }
    return gamma * epsilon;
}

static int Day3Pt2(int[] readings)
{
    var list = readings.ToList();
    var bits = Convert.ToString(readings.Max(), 2).Length - 1;
    for (var bitNumber = bits; bitNumber >= 0; bitNumber--)
    {
        var bit = 1 << bitNumber;
        int ones = 0, zeros = 0;
        foreach (var y in list)
        {
            if ((y & bit) > 0)
            {
                ones++;
            }
            else
            {
                zeros++;
            }
        }
        var toRemove = ones < zeros ? bit : 0;

        var list1 = list.Where(x => (x & bit) == toRemove);
        list.RemoveAll(x => (list1.Contains(x)));
        list.TrimExcess();
        if (list.Count == 1)
        {
            break;
        }
    }
    var gamma = list[0];
    list = readings.ToList();
    for (var bitNumber = bits; bitNumber >= 0; bitNumber--)
    {
        var bit = 1 << bitNumber;
        int ones = 0, zeros = 0;
        foreach (var y in list)
        {
            if ((y & bit) > 0)
            {
                ones++;
            }
            else
            {
                zeros++;
            }
        }
        var toRemove = ones < zeros ? bit : 0;

        var list1 = list.Where(x => (x & bit) == toRemove);
        list.RemoveAll(x => (!list1.Contains(x)));
        list.TrimExcess();

        if (list.Count == 1)
        {
            break;
        }
    }
    var epsilon = list[0];
    return gamma * epsilon;
}