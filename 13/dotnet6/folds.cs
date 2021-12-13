var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day13Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Part2(input);

int Part1(string[] lines)
{
    var dots = new List<(int, int)>();
    var folds = new List<string>();
    // Parse the input into points and folds
    foreach (var line in lines)
    {
        var temp = line.Split(',');
        if (temp.Length == 2)
        {
            dots.Add((int.Parse(temp[0]), int.Parse(temp[1])));
        }
        else
        {
            temp = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length > 1)
            {
                folds.Add(temp[^1]);
            }
        }
    }

    // Get the paper size
    var height = dots.Max(x => x.Item2) + 1;
    var width = dots.Max(x => x.Item1) + 1;

// Process the first fold only
    var fold = folds[0];
    int xFold, yFold;
    if (fold[0] == 'y')
    {
        // Folding upwards
        yFold = int.Parse(fold.Split('=')[^1]);
        xFold = int.MaxValue; // Set the X fold to an impossible number
    }
    else
    {
        // Folding left
        xFold = int.Parse(fold.Split('=')[^1]);
        yFold = width; // Set the Y fold to an impossible number
    }

    var newDots = new List<(int, int)>();
    foreach (var (dotX, dotY) in dots)
    {
        if (dotX > xFold) // Should the dot be folded left?
        {
            newDots.Add((xFold - (dotX - xFold), dotY));
        }
        else
        {
            // Fold the dot upwards if it's below the fold, else leave as is
            newDots.Add(dotY > yFold ? (dotX, yFold - (dotY - yFold)) : (dotX, dotY));
        }
    }
    return newDots.Distinct().Count(); // How many distinct dots are there?
}

void Part2(string[] lines)
{
    var dots = new List<(int, int)>();
    var folds = new List<string>();
    // Parse the input into points and folds
    foreach (var line in lines)
    {
        var temp = line.Split(',');
        if (temp.Length == 2)
        {
            dots.Add((int.Parse(temp[0]), int.Parse(temp[1])));
        }
        else
        {
            temp = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length > 1)
            {
                folds.Add(temp[^1]);
            }
        }
    }

    int height = 0, width = 0; // Vars for the paper size

    foreach (var fold in folds)
    {
        int xFold; // Left fold position
        int yFold; // Upwards fold position
        if (fold[0] == 'y')
        {
            // Folding upwards
            yFold = int.Parse(fold.Split('=')[^1]);
            xFold = int.MaxValue; // Set the left fold to well beyond the page
        }
        else
        {
            // Folding left
            xFold = int.Parse(fold.Split('=')[^1]);
            yFold = int.MaxValue; // Set the upwards fold to well beyond the page
        }

        var newDots = new List<(int, int)>(); // Temp list of folded dots
        foreach (var (dotX, dotY) in dots) // Loop through each dot
        {
            if (dotX > xFold) // Does the dot need folding left?
            {
                newDots.Add((xFold - (dotX - xFold), dotY));
            }
            else
            {
                // Fold the dot upwards if it's below the fold, else leave as is
                newDots.Add(dotY > yFold ? (dotX, yFold - (dotY - yFold)) : (dotX, dotY));
            }
        }
        dots.Clear(); // Clear the old dots list
        dots.AddRange(newDots.Distinct()); // And copy the transformed dots
    }

    height = dots.Max(x => x.Item2) + 1;
    width = dots.Max(x => x.Item1) + 1;
    var plot = new int[width, height]; // Dot matrix for output
    foreach (var (dotX, dotY) in dots)
    {
        plot[dotX, dotY] = 1;
    }

    // Print out the result
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            Console.Write(plot[x, y] > 0 ? '#' : '.');
        }
        Console.WriteLine();
    }
}