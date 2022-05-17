import os
import sys

balls = [""]
boards = [[[0]]]
board_count = 0  # Variable for the number of bingo boards
row_count = 0  # Var for the count of rows per board
column_count = 0  # Var for the count of columns per board
losing_boards = [0]  # List of the boards that haven't won yet
board = 0  # counter for the board being processed


def set_vars(lines):
    global balls, board_count, boards, column_count, losing_boards, row_count
    # Calculate the number of boards from the blank lines preceding them
    board_count = 0
    for line in lines:
        if line.split() == []:
            board_count += 1
    # Calculate the number of rows per board
    row_count = int((len(lines) - 1) / board_count - 1)
    column_count = len(lines[2].split(' '))  # and columns
    boards = [[[0 for _ in range(column_count)] for _ in range(
        row_count)] for _ in range(board_count)]
    losing_boards.clear()
    for board_no in range(0, board_count):  # Make sure all boards are in play
        losing_boards.append(board_no)
    balls = lines[0].split(',')  # Top line has the balls drawn
    # Populate each board from the input data
    for card in range(0, board_count):
        for line in range(0, row_count):
            numbers = lines[2 + (card * (row_count + 1)) + line].split(None)
            for number in range(0, column_count):
                boards[card][line][number] = int(numbers[number])


def check_numbers(ball):
    for board in range(0, board_count):
        for line in range(0, row_count):
            for number in range(0, column_count):
                if boards[board][line][number] == ball:
                    boards[board][line][number] = -1


def check_boards(part2=False):
    result = -1
    for board in range(0, board_count):
        for line in range(0, row_count):
            line_sum = 0
            for number in range(0, column_count):
                line_sum += boards[board][line][number]
            if line_sum != -column_count:
                continue
            if part2:  # Extra logic for Part 2
                if losing_boards.count(board) <= 0:
                    continue
                # Remove the winning board from the in play list
                losing_boards.pop(losing_boards.index(board))
                result = board  # If a new board has won then return the board number
            else:
                return board
        for column in range(0, column_count):
            line_sum = 0
            for line in range(0, row_count):
                line_sum += boards[board][line][column]
            if line_sum != -row_count:
                continue
            if part2:
                if losing_boards.count(board) <= 0:
                    continue
                # Remove the winning board from the in play list
                losing_boards.pop(losing_boards.index(board))
                result = board
            else:
                return board
    return result


def part1(readings):
    global balls
    set_vars(readings)
    fullline = False  # Flag for when we have a winning line
    ballpointer = 0  # Reset the ball counter
    board = 0  # Which board we're looking at
    ball = 0  # The most recent ball value
    while not fullline and ballpointer < len(balls):
        ball = int(balls[ballpointer])
        ballpointer += 1
        check_numbers(ball)
        board = check_boards()
        if board >= 0:
            fullline = True
    boards_sum = 0
    for line in range(0, row_count):
        for number in range(0, column_count):
            boards_sum += boards[board][line][number] if boards[board][line][number] >= 0 else 0
    return boards_sum * ball


def part2(readings):

    # Part 2
    set_vars(readings)
    fullline = False  # Flag for when we have a winning line
    ballpointer = 0  # Reset the ball counter
    board = 0  # Which board we're looking at
    ball = 0  # The most recent ball value
    while len(losing_boards) > 0:
        fullline = False
        while not fullline and ballpointer < len(balls):
            ball = int(balls[ballpointer])
            ballpointer += 1
            check_numbers(ball)
            board = check_boards(True)
            if board >= 0:
                fullline = True
    boards_sum = 0
    for line in range(0, row_count):
        for number in range(0, column_count):
            boards_sum += boards[board][line][number] if boards[board][line][number] >= 0 else 0
    return boards_sum * ball


def main():
    file_path = os.path.join(os.path.dirname(__file__), "input.txt")
    with open(file_path, "+rt", encoding='utf-8-sig') as f:
        lines = f.readlines()
    print("Part 1 ", part1(lines))
    print("Part 2 ", part2(lines))


if __name__ == "__main__":
    sys.exit(int(main() or 0))
