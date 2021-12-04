// Uses .NET 6
var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
var temp = System.IO.File.ReadAllLines($"{Path.GetDirectoryName(path)}\\input.txt");
int[] readings = Array.ConvertAll(temp, int.Parse);
//int[] test = { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

// Part 1
int increases = 0;
int last_depth = 32767;

for (int x = 0; x < readings.Length; x++)
{
    if (readings[x] > last_depth)
        increases++;
    last_depth = readings[x];
}
Console.WriteLine($"depth increases {increases} times.");

// Part 2
last_depth = 32767;
increases = 0;
for (int x = 0; x < readings.Length - 2; x++)
{
    int window1 = readings[x] + readings[x + 1] + readings[x + 2];
    if (window1 > last_depth)
    {
        increases++;
    }
    last_depth = window1;
}
Console.WriteLine($"depth increases {increases} times.");
