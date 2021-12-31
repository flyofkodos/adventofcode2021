int Die = 0;
int RollCount = 0;

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day21Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] lines)
{
    var Player1Space = int.Parse(lines[0].Split(':')[^1]);
    var Player2Space = int.Parse(lines[1].Split(':')[^1]);
    var Player1Score = 0;
    var Player2Score = 0;
    while (Player1Score < 1000 && Player2Score < 1000)
    {
        Player1Space = Roll(Player1Space);
        Player1Score += Player1Space;
        if (Player1Score >= 1000)
        {
            break;
        }
        Player2Space = Roll(Player2Space);
        Player2Score += Player2Space;
    }
    return Math.Min(Player1Score, Player2Score) * RollCount;
}
int Roll(int Place)
{
    for (var roll = 0; roll < 3; roll++)
    {
        RollCount++;
        if (++Die > 100)
        {
            Die = 1;
        }
        Place += Die;
        while (Place > 10)
        {
            Place -= 10;
        }
    }
    return Place;
}
int Part2(string[] lines)
{
    return -1;
}