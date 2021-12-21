var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllText($"{path}\\Day17Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string input)
{
    var temp = input.Split(':')[1].Split(',');
    var ystuff = temp[1].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
    var bottom = int.Parse(ystuff[0]);
    var dy = (-bottom) - 1; // Highest value to end at the bottom of the target area
    return (dy * (dy + 1) / 2); // Return the sum of the positive vertical velocities
}

int Part2(string input)
{
    var temp = input.Split(':')[1].Split(',');
    var xstuff = temp[0].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
    var left = int.Parse(xstuff[0]);
    var right = int.Parse(xstuff[1]);
    var ystuff = temp[1].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
    var bottom = int.Parse(ystuff[0]);
    var top = int.Parse(ystuff[1]);
    var minDx = (int)Math.Floor(Math.Sqrt(left * 2.0d)); // Lowest x velocity that will reach the target
    var maxDy = (-bottom) - 1; // Highest value to end at the bottom of the target area
    var result = 0; // Counter for the shots on target
    // Brute-force loop through every possible x velocity
    for (var xVelocity = minDx; xVelocity <= right; xVelocity++)
    {
        // Brute-force for every possible y velocity
        for (var yVelocity = bottom; yVelocity <= maxDy; yVelocity++)
        {
            var x = 0; // Reset the starting point
            var y = 0;
            var dx = xVelocity;
            var dy = yVelocity;
            while (y > top || x < left) // Loop while we're above or to the left of the target
            {
                x += dx--;
                y += dy--;
                if (dx < 0)
                {
                    dx = 0;
                }
                if (y < bottom || x > right) // break if we've overshot
                {
                    break;
                }
            }
            if (x >= left && x <= right && y <= top && y >= bottom)
            {
                // Increment the shot count if we're inside the target area
                result++;
            }
        }
    }
    return result;
}
