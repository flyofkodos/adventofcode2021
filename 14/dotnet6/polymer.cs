var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day14Input.txt");
Console.WriteLine($"Part 1 is {Part1(input, 10)}");
Console.WriteLine($"Part 1 is {Part1(input, 40)}");

long Part1(string[] lines, int steps)
{
    var polymer = lines[0];
    var rules = new List<(string, string, string)>();
    var toReplace = new List<(string, string, string)>();
    for (var x = 2; x < lines.Length; x++) // Parse the insert rules
    {
        // Each rule is current, prepend, postpend
        var temp = lines[x].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var prepend = string.Concat(temp[0][0], temp[^1][0]); // Start of new polymer
        rules.Add((temp[0], prepend, string.Concat(temp[^1][0], temp[0][1])));
    }
    var pairs = StringToPairs(polymer); // Convert the string to a list of pairs
    for (var step = 1; step <= steps; step++)
    {
        toReplace.Clear(); // Make sure the list of replacements is empty
        var newPairs = new List<(string, long)>(); // List for the new polymers to add
        toReplace.AddRange(from rule in rules let element = pairs.FirstOrDefault(p => p.Item1 == rule.Item1) where element.Item1 != null select rule);
        foreach (var (current, pre, post) in toReplace)
        {
            var origElement = pairs.FirstOrDefault(p => p.Item1 == current);
            var toAdd = origElement.Item2; // Get the number of polymers to add

            // Check if first half of the new polymer is already being added
            var element = newPairs.FirstOrDefault(p => p.Item1 == pre);
            newPairs.Add((pre, element.Item2 + toAdd)); // Add the updated tally
            newPairs.Remove(element); // Remove the old tally if needed
                                      // Check if second half of the new polymer is already being added
            element = newPairs.FirstOrDefault(p => p.Item1 == post);
            newPairs.Add((post, element.Item2 + toAdd)); // Update the tally
            newPairs.Remove(element); // Remove the old tally if needed
        }

        // Remove all the original monomers so we don't get duplicates
        foreach (var rec in from rec in toReplace let element = newPairs.FirstOrDefault(n => n.Item1 == rec.Item1) select rec)
        {
            pairs.RemoveAll(p => p.Item1 == rec.Item1);
        }
        pairs.AddRange(newPairs); // Add the new list of monomers to the original
    }

    var chars = new List<char>(); // List if elements to tally scores
    foreach (var pair in pairs) // Add each element from each pair
    {
        chars.AddRange(pair.Item1.ToCharArray());
    }
    // Make a distinct list of elements
    var charList = chars.Distinct().ToList();
    charList.Sort();
    var scores = new List<(char, long)>();
    foreach (var (monomer, total) in pairs)
    {
        var score = scores.FirstOrDefault(s => s.Item1 == monomer[0]);
        if (score.Item1 == '\0') // If not already in the list
        {
            scores.Add((monomer[0], total)); // Add the tally for character 1
        }
        else
        {
            // Or update the tally if it's in the list
            scores.Add((monomer[0], score.Item2 + total));
            scores.Remove(score);
        }
        score = scores.FirstOrDefault(s => s.Item1 == monomer[1]);
        if (score.Item1 == '\0') // If not in the list already
        {
            scores.Add((monomer[1], total)); // Add the tally for character 2
        }
        else
        {
            // Or update the tally if it's in the list
            scores.Add((monomer[1], score.Item2 + total));
            scores.Remove(score);
        }
    }

    var sorted = scores.OrderByDescending(p => p.Item2).ToList();
    // No idea why yet, but we need to halve the totals to get the real count
    var retDouble = Math.Ceiling((double)sorted[0].Item2 / 2) - Math.Ceiling((double)sorted[^1].Item2 / 2);
    var t = Convert.ToInt64(retDouble); // Ceiling needs a double type so convert back to long
    return t;
}

// Method to convert a string to a list of character pairs
List<(string, long)> StringToPairs(string polymer)
{
    var pairs = new List<(string, long)>();
    for (var x = 0; x < polymer.Length - 1; x++) // Step through each 2 char chunk
    {
        var pair = polymer.Substring(x, 2); // Get the current pair
        var element = pairs.FirstOrDefault(p => p.Item1 == pair); // Check for existing pairs
        pairs.Add((pair, element.Item2 + 1)); // Add to the list (Item2 = 0 if the pair isn;t on the list)
        pairs.Remove(element); // Remove the original pair if it exists to stop duplicates
    }
    return pairs; // Return the list of monomers found
}
