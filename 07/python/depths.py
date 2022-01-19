from math import ceil, floor
import os
import sys


def part1(depths):
    # Work out the median for the provided depths
    depths.sort()
    if (len(depths) % 1 == 1):
        optimal_depth = (depths[int(ceil(len(depths)/2))] +
                         depths[int(floor(len(depths)/2))])/2
    else:
        optimal_depth = depths[int(len(depths)/2)]
    # Return the sum of the differences from the optimal depth
    ret_val = 0
    for depth in depths:
        ret_val += abs(depth - optimal_depth)
    return ret_val


def part2(depths):
    fuel_used = []
    depths.sort()
    for test in range(depths[0], depths[-1]):
        fuel = 0
        for depth in depths:
            fuel += abs(depth - test) * (1 + abs(depth - test)) / 2
        fuel_used.append(fuel)
    fuel_used.sort()
    return int(fuel_used[0])


def main():
    file_path = os.path.join(os.path.dirname(__file__), "Day07Input.txt")
    with open(file_path, "+rt", encoding='utf-8-sig') as f:
        lines = f.readline()
    depths = list(map(int, lines.split(',')))
    print("Part 1 ", part1(depths))
    print("Part 2 ", part2(depths))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
