ulong[] Ages = new ulong[9];
const string Real =
    "1,4,3,3,1,3,1,1,1,2,1,1,1,4,4,1,5,5,3,1,3,5,2,1,5,2,4,1,4,5,4,1,5,1,5,5,1,1,1,4,1,5,1,1,1,1,1,4,1,2,5,1,4,1,2,1,1,5,1,1,1,1,4,1,5,1,1,2,1,4,5,1,2,1,2,2,1,1,1,1,1,5,5,3,1,1,1,1,1,4,2,4,1,2,1,4,2,3,1,4,5,3,3,2,1,1,5,4,1,1,1,2,1,1,5,4,5,1,3,1,1,1,1,1,1,2,1,3,1,2,1,1,1,1,1,1,1,2,1,1,1,1,2,1,1,1,1,1,1,4,5,1,3,1,4,4,2,3,4,1,1,1,5,1,1,1,4,1,5,4,3,1,5,1,1,1,1,1,5,4,1,1,1,4,3,1,3,3,1,3,2,1,1,3,1,1,4,5,1,1,1,1,1,3,1,4,1,3,1,5,4,5,1,1,5,1,1,4,1,1,1,3,1,1,4,2,3,1,1,1,1,2,4,1,1,1,1,1,2,3,1,5,5,1,4,1,1,1,1,3,3,1,4,1,2,1,3,1,1,1,3,2,2,1,5,1,1,3,2,1,1,5,1,1,1,1,1,1,1,1,1,1,2,5,1,1,1,1,3,1,1,1,1,1,1,1,1,5,5,1";

Console.WriteLine("Part 1");
SetVars(Real);
Process(80);
Console.WriteLine("Part 2");
SetVars(Real);
Process(256);

void SetVars(string input)
{
    List<sbyte> fish = new();
    fish.AddRange(Array.ConvertAll(input.Split(',').ToArray(), sbyte.Parse));
    for (var age = 0; age < 9; age++)
    {
        Ages[age] = (ulong)fish.Count(i => i == age);
    }
}

ulong Process(int days)
{
    for (var day = 0; day < days; day++)
    {
        var birthing = Ages[0];
        for (var age = 0; age < 8; age++)
        {
            Ages[age] = Ages[age + 1];
        }

        Ages[6] += birthing;
        Ages[8] = birthing;
    }

    var sum = Ages.Aggregate<ulong, ulong>(0, (current, age) => current + age);
    Console.WriteLine($"After {days} days there are {sum} fish");
    return sum;
}