import os
import sys


def part1(commands):
    horiz = 0
    depth = 0
    for command in commands:
        parts = command.split(' ')
        magnitude = int(parts[1])
        if parts[0] == "forward":
            horiz += magnitude
        if parts[0] == "backward":
            horiz -= magnitude
        if parts[0] == "down":
            depth += magnitude
        if parts[0] == "up":
            depth -= magnitude
    print("Depth %d pos %d - %d" % (depth, horiz, depth * horiz))
    return depth * horiz


def part2(commands):

    # Part 2
    horiz = 0
    depth = 0
    aim = 0
    for command in commands:
        parts = command.split(' ')
        magnitude = int(parts[1])
        if parts[0] == "forward":
            horiz += magnitude
            depth += (aim * magnitude)
        if parts[0] == "backward":
            horiz -= magnitude
            depth -= (aim * magnitude)
        if parts[0] == "down":
            aim += magnitude
        if parts[0] == "up":
            aim -= magnitude
    print("Depth %d pos %d - %d" % (depth, horiz, depth * horiz))
    return depth * horiz


def main():
    file_path = os.path.join(os.path.dirname(__file__), "input.txt")
    with open(file_path, "+rt") as f:
        lines = f.readlines()
    part1(lines)
    part2(lines)


if __name__ == "__main__":
    sys.exit(int(main() or 0))
