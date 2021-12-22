namespace AdventOfCode;

public class SnailFishTree
{
    private readonly SnailFishTree[] _childNodes = new SnailFishTree[2];
    private readonly int _left = -1;
    private readonly int _level;
    private readonly int _right = -1;

    public SnailFishTree(string line, int level = 1)
    {
        RawString = line;
        _childNodes = new SnailFishTree[2];
        _level = level;
        var stack = new List<(char, int)>();
        var parts = line.ToCharArray();
        var l = _level - 1;
        foreach (var part in parts)
            switch (part)
            {
                case '[':
                    stack.Add((part, ++l));
                    break;
                case ']':
                    stack.Add((part, l--));
                    break;
                default:
                    stack.Add((part, l));
                    break;
            }

        if (stack.Max(x => x.Item2) > _level)
        {
            var start = stack.IndexOf(stack.Find(x => x.Item2 > _level));
            var end = stack.IndexOf(stack.Find(x => x.Item1 == ',' && x.Item2 == level));
            var tempstring = "";

            if (start > 1)
            {
                var numbers = Array.ConvertAll(line.Replace("[", "").Replace("]", "").Split(','), int.Parse);
                _left = numbers[0];
            }
            else
            {
                for (var i = start; i < end; i++) tempstring += stack[i].Item1;
                _childNodes[0] = new SnailFishTree(tempstring, _level + 1);
                _left = _childNodes[0].Magnitude;
            }

            tempstring = "";

            for (var i = end + 1; i < stack.Count; i++)
                if (stack[i].Item2 > _level)
                    tempstring += stack[i].Item1;
            if (tempstring.Length > 0)
            {
                _childNodes[1] = new SnailFishTree(tempstring, _level + 1);
                _right = _childNodes[1].Magnitude;
            }
            else
            {
                var numbers = Array.ConvertAll(line.Replace("[", "").Replace("]", "").Split(','), int.Parse);
                _right = numbers[^1];
            }
        }
        else
        {
            var numbers = Array.ConvertAll(line.Replace("[", "").Replace("]", "").Split(','), int.Parse);
            _left = numbers[0];
            _right = numbers[1];
        }

        Magnitude = 3 * _left + 2 * _right;
    }

    public int Magnitude { get; } = -1;

    public string RawString { get; }

    public static string operator +(SnailFishTree tree1, string line2)
    {
        return $"[{tree1.RawString},{line2}]";
    }

    public static string operator +(SnailFishTree tree1, SnailFishTree tree2)
    {
        return $"[{tree1.RawString},{tree2.RawString}]";
    }
}