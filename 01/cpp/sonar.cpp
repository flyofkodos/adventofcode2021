#include <iostream>
#include <fstream>
#include <vector>

int part1(const std::vector<int>&);
int part2(const std::vector<int>&);

int main()
{
	std::ifstream infile;
	infile.open("input.txt");
	std::vector<int> input{};
	int i;

	while (infile >> i)
	{
		input.push_back(i);
	}
	infile.close();

	std::cout << "Part 1 is " << part1(input) << std::endl;
	std::cout << "Part 2 is " << part2(input) << std::endl;
	return 0;
}

int part1(const std::vector<int>& readings)
{
	int increases = 0;
	int last_depth = 32767;
	for (const int reading : readings)
	{
		if (reading > last_depth)
		{
			increases++;
		}
		last_depth = reading;
	}
	return increases;
}

int part2(const std::vector<int>& readings)
{
	int increases = 0;
	int last_depth = 32767;

	for (unsigned long x = 0; x < readings.size() - 2; x++)
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
