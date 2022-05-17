import os
import sys


def process(input, diagonals=False):
    grid = [[0 for _ in range(1000)] for _ in range(1000)]
    for vent in input:
        parts = vent.split(' ')
        start = list(map(int, parts[0].split(',')))
        end = list(map(int, parts[2].split(',')))
        if start[0] != end[0] and start[1] == end[1]:
            # Horizontal vent
            for x in range(min(start[0], end[0]), 1 + max(start[0], end[0])):
                grid[x][start[1]] += 1
        else:
            if start[0] == end[0] and start[1] != end[1]:
                # Vertical vent
                for y in range(min(start[1], end[1]), 1 + max(start[1], end[1])):
                    grid[start[0]][y] += 1
            else:
                # Check for 45 degree lines if the diagonals flag was passed
                if diagonals and abs(end[0]-start[0]) == abs(end[1]-start[1]):
                    dx = 1 if end[0] > start[0] else -1
                    dy = 1 if end[1] > start[1] else -1
                    x = start[0]
                    y = start[1]
                    while x != end[0] + dx:
                        grid[x][y] += 1
                        x += dx
                        y += dy
    # Return the count of all elements > 1
    retval = 0
    for x in grid:
        retval += sum(y > 1 for y in x)
    return retval


def main():
    file_path = os.path.join(os.path.dirname(__file__), "Day05Input.txt")
    with open(file_path, "+rt", encoding='utf-8-sig') as f:
        lines = f.readlines()
    print("Part 1 ", process(lines))
    print("Part 2 ", process(lines, True))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
