import os
import sys


def part1(readings):
    bits = len(bin(max(readings))) - 2
    gamma = 0
    epsilon = 0
    for x in range(bits - 1, -1, -1):
        bit = 1 << x
        ones = 0
        zeros = 0
        for y in readings:
            if y & bit > 0:
                ones += 1
            else:
                zeros += 1
        if ones > zeros:
            gamma += bit
        else:
            epsilon += bit
    return gamma * epsilon


def part2(readings):

    # Part 2
    bits = len(bin(max(readings))) - 2
    temp_list = []
    for item in readings:
        temp_list.append(item)
    list1 = []
    for x in range(bits - 1, -1, -1):
        bit = 1 << x
        ones = 0
        zeros = 0
        for y in temp_list:
            if y & bit > 0:
                ones += 1
            else:
                zeros += 1
        if ones < zeros:
            to_remove = bit
        else:
            to_remove = 0
        list1.clear()
        for reading in temp_list:
            if reading & bit != to_remove:
                list1.append(reading)
        temp_list.clear()
        for item in list1:
            temp_list.append(item)
        if len(temp_list) == 1:
            break
    gamma = temp_list[0]

    temp_list.clear()
    for item in readings:
        temp_list.append(item)
    for x in range(bits - 1, -1, -1):
        bit = 1 << x
        ones = 0
        zeros = 0
        for y in temp_list:
            if y & bit > 0:
                ones += 1
            else:
                zeros += 1
        if ones < zeros:
            to_remove = bit
        else:
            to_remove = 0
        list1.clear()
        for reading in temp_list:
            if reading & bit == to_remove:
                list1.append(reading)
        temp_list.clear()
        for item in list1:
            temp_list.append(item)
        if len(temp_list) == 1:
            break
    epsilon = temp_list[0]  # should be 10 in test
    return gamma * epsilon


def main():
    file_path = os.path.join(os.path.dirname(__file__), "input.txt")
    with open(file_path, "+rt") as f:
        lines = f.readlines()
        int_list = [int(i, 2) for i in lines]
    print("Part 1 %d" % (part1(int_list)))
    print("Part 2 %d" % (part2(int_list)))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
