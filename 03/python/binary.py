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
    tempList = []
    for item in readings:
        tempList.append(item)
    list1 = []
    for x in range(bits - 1, -1, -1):
        bit = 1 << x
        ones = 0
        zeros = 0
        for y in tempList:
            if y & bit > 0:
                ones += 1
            else:
                zeros += 1
        if ones < zeros:
            toRemove = bit
        else:
            toRemove = 0
        list1.clear()
        for reading in tempList:
            if reading & bit != toRemove:
                list1.append(reading)
        tempList.clear()
        for item in list1:
            tempList.append(item)
        if len(tempList) == 1:
            break
    gamma = tempList[0]

    tempList.clear()
    for item in readings:
        tempList.append(item)
    for x in range(bits - 1, -1, -1):
        bit = 1 << x
        ones = 0
        zeros = 0
        for y in tempList:
            if y & bit > 0:
                ones += 1
            else:
                zeros += 1
        if ones < zeros:
            toRemove = bit
        else:
            toRemove = 0
        list1.clear()
        for reading in tempList:
            if reading & bit == toRemove:
                list1.append(reading)
        tempList.clear()
        for item in list1:
            tempList.append(item)
        if len(tempList) == 1:
            break
    epsilon = tempList[0]  # should be 10 in test
    return gamma * epsilon


def main():
    file_path = os.path.join(os.path.dirname(__file__), "input.txt")
    with open(file_path, "+rt") as f:
        lines = f.readlines()
        intList = [int(i, 2) for i in lines]
    print("Part 1 %d" % (part1(intList)))
    print("Part 2 %d" % (part2(intList)))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
