string[] Test;
string[] Real;
string[]? _balls;
int[,,]? _boards;
int _boardCount;
int _rowCount;
int _colCount;
List<int> LosingBoards = new();
int _board = 0;

var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
Test = File.ReadAllLines($"{path}\\Day04Test.txt");
Real = File.ReadAllLines($"{path}\\Day04Input.txt");
SetVars(Test);
ParseInput();
SetVars(Real);
ParseInput();
SetVars(Test);
ParseInput2();
SetVars(Real);
ParseInput2();

void SetVars(string[] lines)
{
    _boardCount = lines.Count(x => x.Length < 5);
    _rowCount = (lines.Length - 1) / _boardCount - 1;
    _colCount = lines[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    _boards = new int[_boardCount, _rowCount, _colCount];
    LosingBoards.Clear();
    for (var x = 0; x < _boardCount; x++)
    {
        LosingBoards.Add(x);
    }
    _balls = lines[0].Split(',');

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
    var fullLine = false;
    var ballPointer = 0;
    var board = 0;
    var ball = 0;
    while (_balls != null && !fullLine && ballPointer < _balls.Length)
    {
        ball = Convert.ToInt32(_balls[ballPointer++]);
        CheckNumbers(ball);
        board = CheckBoards();
        if (board >= 0)
        {
            fullLine = true;
        }
    }
    var sum = 0;
    for (var line = 0; line < _rowCount; line++)
    {
        for (var number = 0; number < _colCount; number++)
        {
            sum += _boards != null && _boards[board, line, number] >= 0 ? _boards[board, line, number] : 0;
        }
    }
    Console.WriteLine($"Total left on board {board} is {sum} * ball {ball} = {sum * ball}");
}
void ParseInput2()
{
    var ballPointer = 0;

    var ball = 0;
    while (LosingBoards.Count > 0)
    {
        var fullLine = false;
        while (_balls != null && !fullLine && ballPointer < _balls.Length)
        {
            ball = Convert.ToInt32(_balls[ballPointer++]);
            CheckNumbers(ball);
            _board = CheckBoards(true);
            fullLine = _board >= 0;
        }
    }
    var sum = 0;
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
                    _boards[board, line, number] = -1;
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
