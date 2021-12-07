#include <iostream>

int part1(int[], int);
int part2(int[], int);

int main()
{
    std::cout << "Hello World!\n";
    int test[] = {199, 200, 208, 210, 200, 207, 240, 269, 260, 263};
    std::cout << "Part 1 test is " << part1(test, std::size(test)) << std::endl;
    std::cout << "Part 2 test is " << part2(test, std::size(test)) << std::endl;
}

int part1(int readings[], const int readings_count)
{
    int increases = 0;
    int last_depth = 32767;

    for (int x = 0; x < readings_count; x++)
    {
        if (readings[x] > last_depth)
        {
            increases++;
        }
        last_depth = readings[x];
    }
    return increases;
}

int part2(int readings[], const int readings_count)
{
    int increases = 0;
    int last_depth = 32767;

    for (int x = 0; x < readings_count; x++)
    {
        const int window1 = readings[x] + readings[x + 1] + readings[x + 2];
        if (window1 > last_depth)
        {
            increases++;
        }
        last_depth = window1;
    }
    return increases;
}