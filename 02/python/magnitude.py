import os
file_path = os.path.dirname(__file__)
with open("%s\\input.txt" % file_path) as f:
    lines = f.readlines()

horiz = 0
depth = 0
for command in lines:
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

# Part 2
horiz = 0
depth = 0
aim = 0
for command in lines:
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
