int _height;
int _width;
var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day15Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] lines)
{
_height = lines.Length;
_width = lines[0].Length;
var vertex = new List<(short x, short y, short Risk, short Distance)>();
var dist = new short[_width * _height];
var prev = new (short x, short y)[_width, _height];
for (short y = 0; y < _height; y++)
{
for (short x = 0; x < _width; x++)
{
vertex.Add((x, y, short.Parse(lines[y][x].ToString()), short.MaxValue));
dist[x + (_width * y)] = short.MaxValue / 2;
prev[x, y] = (-1, -1);
}
}
vertex.RemoveAt(0);
vertex.Add((0, 0, 0, 0));
dist[0] = (short)0;
while (vertex.Count > 0)
{
var temp = short.MaxValue;
short minDist = 0;
// Much quicker than the List.Min() method
foreach (var vert in vertex.Where(vert => vert.Distance < temp))
{
temp = vert.Distance;
minDist = (short)(vert.x + (_width * vert.y));
}

var u = vertex.Where(x => x.x == minDist % _width && x.y == minDist / _width).First();
var v = vertex.Find(a => a.x == u.x && a.y == u.y - 1);
if (v.y == u.y - 1)
{
var alt = (short)(dist[minDist] + v.Risk);
if (alt < dist[v.x + (_width * v.y)])
{
dist[v.x + (_width * v.y)] = alt;
vertex.Remove(v);
v.Distance = alt;
vertex.Add(v);
prev[v.x, v.y] = (u.x, u.y);
}
}
v = vertex.Find(a => a.x == u.x && a.y == u.y + 1);
if (v.y == u.y + 1)
{
var alt = (short)(dist[minDist] + v.Risk);
if (alt < dist[v.x + (_width * v.y)])
{
dist[v.x + (_width * v.y)] = alt;
vertex.Remove(v);
v.Distance = alt;
vertex.Add(v);
prev[v.x, v.y] = (u.x, u.y);
}
}
v = vertex.Find(a => a.x == u.x - 1 && a.y == u.y);
if (v.x == u.x - 1)
{
var alt = (short)(dist[minDist] + v.Risk);
if (alt < dist[v.x + (_width * v.y)])
{
dist[v.x + (_width * v.y)] = alt;
vertex.Remove(v);
v.Distance = alt;
vertex.Add(v);
prev[v.x, v.y] = (u.x, u.y);
}
}
v = vertex.Find(a => a.x == u.x + 1 && a.y == u.y);
if (v.x == u.x + 1)
{
var alt = (short)(dist[minDist] + v.Risk);
if (alt < dist[v.x + (_width * v.y)])
{
dist[v.x + (_width * v.y)] = alt;
vertex.Remove(v);
v.Distance = alt;
vertex.Add(v);
prev[v.x, v.y] = (u.x, u.y);
}
}
vertex.Remove(u);
}
return dist[^1];
}

int Part2(string[] lines)
{
        _height = lines.Length;
        _width = lines[0].Length;
        var dist = new int[25 * _width * _height];
        var riskArray = new int[5 * _width, 5 * _height];
        Array.Fill(dist, short.MaxValue / 2);
        for (short y = 0; y < _height; y++)
        for (short x = 0; x < _width; x++)
        {
            var risk = short.Parse(lines[y][x].ToString());
            for (short dy = 0; dy < 5; dy++)
            for (short dx = 0; dx < 5; dx++)
            {
                var newRisk = (short) (risk + dx + dy);
                if (newRisk > 9) newRisk -= 9;
                riskArray[x + dx * _width, y + dy * _height] = newRisk;
            }
        }

        _height *= 5;
        _width *= 5;
        dist[0] = 0;
        var minDist = 0;
        while (minDist >= 0)
        {
            var temp = short.MaxValue;
            minDist = -1;
            foreach (short d in dist.Where(x => x >= 0 && x < temp))
            {
                temp = d;
                minDist = Array.IndexOf(dist, d);
            }

            if (minDist < 0) continue;
            var ux = minDist % _width;
            var uy = minDist / _width;
            if (ux > 0)
            {
                var alt = dist[minDist] + riskArray[ux - 1, uy];
                if (alt < dist[minDist - 1])
                {
                    dist[minDist - 1] = alt;
                    riskArray[ux - 1, uy] = alt;
                }
            }

            if (ux < _width - 1)
            {
                var alt = dist[minDist] + riskArray[ux + 1, uy];
                if (alt < dist[minDist + 1])
                {
                    dist[minDist + 1] = alt;
                    riskArray[ux + 1, uy] = alt;
                }
            }

            if (uy > 0)
            {
                var alt = dist[minDist] + riskArray[ux, uy - 1];
                if (alt < dist[minDist - _width])
                {
                    dist[minDist - _width] = alt;
                    riskArray[ux, uy - 1] = alt;
                }
            }

            if (uy < _height - 1)
            {
                var alt = dist[minDist] + riskArray[ux, uy + 1];
                if (alt < dist[minDist + _width])
                {
                    dist[minDist + _width] = alt;
                    riskArray[ux, uy + 1] = alt;
                }
            }

            if (minDist < dist.Length - 1)
                dist[minDist] = -1;
            else
                return dist[^1];
        }

        return -1;
}