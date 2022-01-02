using System.Reflection;

var path = Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day25Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");

int Part1(string[] lines)
{
    var width = lines[0].Length;
    var height = lines.Length;
    var moves = 0;
    var toMove = new List<(int, int)>();
    bool moved;
    do
    {
        moved = false;
        toMove.Clear();
        // Loop through all the right-facing cucumbers and move where possible
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                // Use Mod(x, width) to wrap around
                if (lines[y][x] == '>' && lines[y][(x + 1) % width] == '.')
                {
                    toMove.Add((x, y)); // Add to a move list so all move at once
                    moved = true; // Set the loop flag
                }
            }
        }
        // Move all the right-facing cucumbers in the list
        foreach (var (x, y) in toMove)
        {
            var tempLine = lines[y].ToCharArray();
            tempLine[(x + 1) % width] = '>';
            tempLine[x] = '.';
            lines[y] = new string(tempLine);
        }
        toMove.Clear(); // Clear the move list
        // Loop through all the down-facing cucumbers and move where possible
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (lines[y][x] == 'v' && lines[(y + 1) % height][x] == '.')
                {
                    toMove.Add((x, y));
                    moved = true;
                }
            }
        }
        // Move all the down-facing cucumbers on the list
        foreach (var (x, y) in toMove)
        {
            var tempLine = lines[y].ToCharArray();
            tempLine[x] = '.';
            lines[y] = new string(tempLine);
            tempLine = lines[(y + 1) % height].ToCharArray();
            tempLine[x] = 'v';
            lines[(y + 1) % height] = new string(tempLine);
        }
        moves++; // Increment the steps count
    } while (moved); // Keep going while cucumbers are moving
    return moves; // Return the number of steps taken
}