import os
import sys


def part1(readings):
    increases = 0
    last_depth = sys.maxsize

    for reading in readings:
        if reading > last_depth:
            increases = increases + 1
        last_depth = reading
    return increases


def part2(readings):
    increases = 0
    last_depth = sys.maxsize

    for pointer in range(len(readings) - 2):
        window1 = readings[pointer] + \
            readings[pointer + 1] + readings[pointer + 2]
        if window1 > last_depth:
            increases = increases + 1
        last_depth = window1
    return increases


def main():
    file_path = os.path.join(os.path.dirname(__file__), "input.txt")
    with open(file_path) as f:
        readings = f.readlines()
    readings = [int(reading) for reading in readings]
    print("Part 1 %d" % part1(readings))
    print("Part 2 %d" % part2(readings))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
