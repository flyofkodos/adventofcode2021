var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day08Input.txt").ToArray();

Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 1 is {Part2(input)}");

int Part1(string[] lines)
{
    return (from line in lines select line.Split('|') into output select output[1].Split(' ') into tokens select tokens.Count(token => token.Length is 2 or 3 or 4 or 7)).Sum();
}

int Part2(string[] lines)
{
    return (from line in lines let numberString = "" let digits = DecodeDigits(line) select line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate(numberString, (current, part) => current + digits.IndexOf(string.Concat(part.OrderBy(c => c)))) into numberString select int.Parse(numberString)).Sum();
}

List<string> DecodeDigits(string input)
{
    var encoded = new List<string>();
    var decoded = new string[10]; // Array for decoded strings
    var segments = new string[7]; // Array to keep track of decoded segments
    // Populate encoded with the tokens from the input
    encoded.AddRange(input.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    encoded.Remove("|"); // Get rid of the delimiter

    var temp = encoded.First(x => x.Length == 2); // Get digit 1
    decoded[1] = temp; // Store the decoded '1' digit
    encoded.RemoveAll(x => x.Length == 2); // Remove digit 1 from the todo list

    temp = encoded.First(x => x.Length == 4); // Get digit 4
    decoded[4] = temp;
    encoded.RemoveAll(x => x.Length == 4); // Remove digit 4 from the todo list

    temp = encoded.First(x => x.Length == 3); // Get digit 7
    decoded[7] = temp;
    encoded.RemoveAll(x => x.Length == 3); // Remove digit 7 from the todo list

    temp = encoded.First(x => x.Length == 7); // Get digit 8
    decoded[8] = temp;
    encoded.RemoveAll(x => x.Length == 7); // Remove digit 8 from the todo list

    // The top segment is the difference between 1 and 7
    segments[0] = decoded[7].Where(c => !decoded[1].Contains(c)).Aggregate("", (current, c) => current + c);
    // Digit 6 is the only 6 char token that doesn't contain both segments for digit 1
    foreach (var dec in encoded.Where(x => x.Length == 6))
    {
        var remaining = decoded[1].Where(c => !dec.Contains(c)).Aggregate("", (current, c) => current + c);
        if (remaining.Length != 1) continue;
        decoded[6] = dec;
        segments[2] = remaining; // Top-right segment is left over
        encoded.Remove(dec);  // Remove digit 6 from the todo list
        break;
    }

    // Work out bottom-right segment from digit 1 minus top-right segment
    segments[5] = decoded[1].Replace(segments[2], "");

    // Digit 2 doesn't have bottom-right segment
    decoded[2] = encoded.Where(x => x.Length == 5).First(y => !y.Contains(segments[5]));
    encoded.Remove(decoded[2]); // Remove digit 2 from the todo list

    // Digit 5 doesn;t have top-right segment
    decoded[5] = encoded.Where(x => x.Length == 5).First(y => !y.Contains(segments[2]));
    encoded.Remove(decoded[5]); // Remove digit 5 from the todo list

    // Digit 3 is the only 5 char entry left
    decoded[3] = encoded.First(x => x.Length == 5);
    encoded.Remove(decoded[3]); // Remove digit 3 from the todo list

    // Zero is the only remaining 6 char entry that doesn't contain all segments from 4
    foreach (var dec in encoded.Where(x => x.Length == 6))
    {
        var remaining = decoded[4].Where(c => !dec.Contains(c)).Aggregate("", (current, c) => current + c);
        if (remaining.Length != 1) continue;
        decoded[0] = dec;
        encoded.Remove(dec); // Remove digit 10 from the todo list
        break;
    }

    decoded[9] = encoded[0]; // 9 is the only digit left

    for (var d = 0; d < 10; d++) // Sort the decoded digits to make matching easier
    {
        decoded[d] = string.Concat(decoded[d].OrderBy(c => c));
    }
    return decoded.ToList(); // Return the decoded list
}
