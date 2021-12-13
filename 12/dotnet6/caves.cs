var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
path = Path.GetDirectoryName(path);
var input = File.ReadAllLines($"{path}\\Day12Input.txt");
Console.WriteLine($"Part 1 is {Part1(input)}");
Console.WriteLine($"Part 2 is {Part2(input)}");

int Part1(string[] paths)
{
    var pathList = paths.Select(path => path.Split('-')).Select(parts => (parts[0], parts[1])).ToList();

    pathList.AddRange(pathList.Select(ret => (ret.Item2, ret.Item1)).ToList());
    pathList.RemoveAll(x => x.Item2 == "start");
    pathList.RemoveAll(x => x.Item1 == "end");
    var routesTried = pathList.Where(x => x.Item1 == "start" || x.Item2 == "start").Select(path => string.Join(',', path.Item1, path.Item2)).ToList();
    var stillGoing = true;
    while (stillGoing)
    {
        stillGoing = false;
        var routeCount = routesTried.Count;
        for (var x = 0; x < routeCount; x++)
        {
            var currentRoute = routesTried[x];
            var next = pathList.Where(p => p.Item1 == currentRoute.Split(',')[^1]);
            foreach (var (_, item2) in next)
            {
                if (item2 == item2.ToLower() && currentRoute.Split(',').Contains(item2))
                {
                    continue;
                }

                stillGoing = true;
                routesTried.Add(string.Join(',', currentRoute, item2));
                routesTried[x] = "";
            }
        }
        routesTried.RemoveAll(x => x == "");
    }
    return routesTried.Count(x => x.EndsWith(",end"));
}
long Part2(string[] paths)
{
    var pathList = paths.Select(path => path.Split('-')).Select(parts => (parts[0], parts[1])).ToList();

    pathList.AddRange(pathList.Select(ret => (ret.Item2, ret.Item1)).ToList());
    pathList.RemoveAll(x => x.Item2 == "start");
    pathList.RemoveAll(x => x.Item1 == "end");
    var small = (from testSmall in pathList where testSmall.Item1 != "start" && testSmall.Item1 == testSmall.Item1.ToLower() select testSmall.Item1).ToList().Distinct();
    var allRoutes = new List<string>();
    foreach (var smallCave in small)
    {
        var routesTried = pathList.Where(x => x.Item1 == "start" || x.Item2 == "start").Select(path => string.Join(',', path.Item1, path.Item2)).ToList();
        var stillGoing = true;
        while (stillGoing)
        {
            stillGoing = false;
            var routeCount = routesTried.Count;
            for (var x = 0; x < routeCount; x++)
            {
                var currentRoute = routesTried[x];
                var next = pathList.Where(p => p.Item1 == currentRoute.Split(',')[^1]);
                foreach (var (_, item2) in next)
                {
                    if (item2 == item2.ToLower() && currentRoute.Split(',').Contains(item2))
                    {
                        // Are we in the special cave
                        if (item2 != smallCave)
                        {
                            continue;
                        }
                        if (currentRoute.Split(',').Count(c => c == smallCave) == 2)
                        {
                            continue;
                        }
                    }
                    stillGoing = true;
                    routesTried.Add(string.Join(',', currentRoute, item2));
                    routesTried[x] = "";
                }
            }
            routesTried.RemoveAll(x => x == "");
        }
        allRoutes.AddRange(routesTried.Where(x => x.EndsWith(",end")));
    }
    return allRoutes.Distinct().Count();
}