var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day10Input.txt").ToArray();
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

decimal Part1(string[] lines)
{
    return lines.Sum(line => LineScore(line, false));
}
decimal Part2(string[] lines)
{
    var scores = lines.Select(line => LineScore(line, true)).ToList();

    scores.RemoveAll(score => score > 0); // Remove the corrupt lines
    scores.Sort(); // Sort the remaining scores
    return -scores[(int)Math.Floor((decimal)scores.Count / 2)]; // Return the Median value
}

decimal LineScore(string line, bool part2 = false)
{
    var stack = new List<char>(); // Stack to keep track of bracket pairs
    int[] scores = { 3, 57, 1197, 25137 }; // Array of scores
    const string check = "([{<>}])"; // String of brackets in score order
    var chars = line.ToList(); // Cast to a list to make processing easier

    // Loop through each char in the line
    for (var x = 0; x < chars.Count; x++)
    {
        var t = chars[x];
        var bracket = check.IndexOf(t); // Get the bracket type
        if (bracket < check.Length / 2) // 1st half means openng bracket
        {
            stack.Add(check[^(bracket + 1)]); // Add the closing bracket to the stack
        }
        else
        {
            if (stack[^1] == t) // Has the most recent bracket pair been closed?
            {
                stack.RemoveAt(stack.Count - 1); // Remove it if yes
            }
            else
            {
                if (stack.Count < x) // Have we closed at least 1 bracket?
                {
                    return scores[(check.Length - 1) - check.IndexOf(t)];
                }
            }
        }
    }

    if (!part2) return 0; // Return if we aren't processing incompletes
    // Calculate the score for the incomplete line
    decimal score = 0; // Needs to be huge or it overflows
    for (var x = 1; x <= stack.Count; x++) // Loop through the remaining stack
    {
        var ending = stack[^x]; // Get the top item
        score *= 5; // Multiply the score by 5
        score += check.Length - check.IndexOf(ending); // Add the score
    }
    // Return a negative value to differentiate the incomplete lines
    return -score;
}
