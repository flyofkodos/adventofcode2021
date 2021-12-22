using System.Reflection;

var path = Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day18Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] lines)
{
    var calc = lines.Aggregate("", AddLines);
    Console.WriteLine(calc);
    return -1;
}

int Part2(string[] lines)
{
    return -1;
}

string AddLines(string line1, string line2)
{
    return $"[{line1},{line2}]";
}

string Reduce(string line)
{
    return line;
}

string Explode(string line)
{
    if (!line.Contains("[[[[[")) return line; // Return if there's nothing to do
    var retVal = "";
    var brackets = 0;
    var toDo = true;
    while (toDo)
    {
        var numberCount = 0;
        var numberList = new List<int>();
        var numString = Array.ConvertAll(line.Replace("[", "").Replace("]", "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        toDo = false;
        var c = 0;
        while (c < line.Length)
        {
            var ch = line[c];
            switch (ch)
            {
                case '[':
                    if (brackets > 3)
                    {
                        toDo = true;
                        var pos = line.IndexOf(']', c);
                        var leftStuff = line[..c].Split('[', StringSplitOptions.RemoveEmptyEntries);
                        var rightStuff = line[(1 + pos)..].Split(']', StringSplitOptions.RemoveEmptyEntries);
                        var values = Array.ConvertAll(line[(c + 1)..pos].Split(','), int.Parse);
                        numberCount += 2;
                        var left = numberCount > 2 ? values[0] + numString[numberCount - 3] : 0;
                        var right = numberCount < numString.Length - 1 ? values[1] + numString[numberCount] : 0;
                        retVal += $"{left},{right}";
                        c = pos;
                    }
                    else
                    {
                        brackets++;
                        retVal += ch;
                        c++;
                    }

                    break;
                case ']':
                    brackets--;
                    retVal += ch;
                    c++;
                    break;
                default:
                    if (char.IsDigit(ch)) numberCount++;
                    retVal += ch;
                    c++;
                    break;
            }
        }
    }

    return retVal;
}