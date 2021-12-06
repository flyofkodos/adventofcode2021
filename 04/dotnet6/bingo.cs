string[] Test; // String array for the values from the example
string[] Real; // String array for the real data
string[]? _balls; // Array for the balls (could change to int[] and parse after loading)
int[,,]? _boards; // 3D array for the boards and their numbers. [boards, rows, columns]
int _boardCount; // Var for the total number of bingo boards
int _rowCount; // Var for the count of rows per board
int _colCount; // Var for the count of columns per board
List<int> LosingBoards = new(); // List of the boards that haven't won yet
int _board = 0; // counter for the board being processed

// Get the executable path so we know where to load the data files from
var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
// Load the test data for sanity checking
Test = File.ReadAllLines($"{path}\\Day04Test.txt");
// Load the real data
Real = File.ReadAllLines($"{path}\\Day04Input.txt");
SetVars(Test); // Load the test data
ParseInput(); // and process it for part 1
SetVars(Real); // Load the actual data
ParseInput(); // and process it for part 1
SetVars(Test); // Reload the test data
ParseInput2(); // and process for part 2
SetVars(Real); // Reload the actual data
ParseInput2(); // and process for part 2

void SetVars(string[] lines)
{
    // Calculate how many boards, rows & columns we have
    _boardCount = lines.Count(x => x.Length < 5); // Calculate the number of boards from the blank lines preceding them
    _rowCount = (lines.Length - 1) / _boardCount - 1; // Calculate the number of rows per board
    _colCount = lines[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length; // and columns
    _boards = new int[_boardCount, _rowCount, _colCount]; // Resize the board array accordingly
    LosingBoards.Clear(); // Make sure all boards are in play
    for (var x = 0; x < _boardCount; x++)
    {
        LosingBoards.Add(x); // This could probably be a linq query
    }
    _balls = lines[0].Split(','); // Top line has the balls drawn

    // Populate each board from the input data
    for (var board = 0; board < _boardCount; board++)
    {
        for (var line = 0; line < _rowCount; line++)
        {
            var numbers = lines[2 + (board * (_rowCount + 1)) + line].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (var number = 0; number < _colCount; number++)
            {
                _boards[board, line, number] = Convert.ToInt32(numbers[number]);
            }
        }
    }
}

void ParseInput()
{
    var fullLine = false; // Flag for when we have a winning line
    var ballPointer = 0; // Reset the ball counter
    var board = 0; // Which board we're looking at
    var ball = 0; // The most recent ball value
    while (_balls != null && !fullLine && ballPointer < _balls.Length)
    {
        ball = Convert.ToInt32(_balls[ballPointer++]);
        CheckNumbers(ball); // Check each board for the current ball
        board = CheckBoards(); // Check all boards for a winning line
        if (board >= 0)
        {
            fullLine = true; // We have a winner
        }
    }
    var sum = 0;
    for (var line = 0; line < _rowCount; line++)
    {
        // Total each remaining number from the winning board
        for (var number = 0; number < _colCount; number++)
        {
            sum += _boards != null && _boards[board, line, number] >= 0 ? _boards[board, line, number] : 0;
        }
    }
    Console.WriteLine($"Total left on board {board} is {sum} * ball {ball} = {sum * ball}");
}
void ParseInput2()
{
    var ballPointer = 0; // Reset the ball counter

    var ball = 0; // The most recent ball value
    while (LosingBoards.Count > 0) // Keep going until all boards have won
    {
        var fullLine = false; // Flag for when we have a winning line
        while (_balls != null && !fullLine && ballPointer < _balls.Length)
        {
            ball = Convert.ToInt32(_balls[ballPointer++]);
            CheckNumbers(ball); // Check each board for the current ball
            _board = CheckBoards(true); // Check all boards for a winning line
            fullLine = _board >= 0; // Check if we have a new winning board
        }
    }
    var sum = 0;
    // Add up each remaining number from the last winning board
    for (var line = 0; line < _rowCount; line++)
    {
        for (var number = 0; number < _colCount; number++)
        {
            sum += _boards != null && _boards[_board, line, number] >= 0 ? _boards[_board, line, number] : 0;
        }
    }
    Console.WriteLine($"Total left on board {_board} is {sum} * ball {ball} = {sum * ball}");
}
void CheckNumbers(int ball)
{
    for (var board = 0; board < _boardCount; board++)
    {
        for (var line = 0; line < _rowCount; line++)
        {
            for (var number = 0; number < _colCount; number++)
            {
                if (_boards != null && _boards[board, line, number] == ball)
                {
                    _boards[board, line, number] = -1; // Set to -1 when a number is drawn
                }
            }
        }
    }
}

int CheckBoards(bool part2 = false)
{
    var result = -1;
    for (var board = 0; board < _boardCount; board++)
    {
        for (var line = 0; line < _rowCount; line++)
        {
            var lineSum = 0;
            for (var number = 0; number < _colCount; number++)
            {
                if (_boards != null) lineSum += _boards[board, line, number];
            }

            if (lineSum != -(_colCount)) continue;
            if (part2) // Extra logic for Part 2
            {
                if (LosingBoards.FindIndex(x => x.Equals(board)) < 0) continue;
                LosingBoards.Remove(board); // Remove the winning board from the in play list
                result = board; // If a new board has won then return the board number
            }
            else
            {
                return board; // Just return the winning board for Part 1
            }
        }
        for (var column = 0; column < _colCount; column++)
        {
            var lineSum = 0;
            for (var line = 0; line < _rowCount; line++)
            {
                if (_boards != null) lineSum += _boards[board, line, column];
            }

            if (lineSum != -(_rowCount)) continue;
            if (part2)
            {
                if (LosingBoards.FindIndex(x => x.Equals(board)) < 0) continue;
                LosingBoards.Remove(board);
                result = board;
            }
            else
            {
                return board;
            }
        }
    }
    return result;
}
