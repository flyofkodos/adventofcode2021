import unittest
from magnitude import part1, part2


class Test_day02_test(unittest.TestCase):

    def test_part1(self):
        test_data = ["forward 5", "down 5",
                     "forward 8", "up 3", "down 8", "forward 2"]
        self.assertEqual(150, part1(test_data))

    def test_part2(self):
        test_data = ["forward 5", "down 5",
                     "forward 8", "up 3", "down 8", "forward 2"]
        self.assertEqual(900, part2(test_data))


if __name__ == '__main__':
    unittest.main()
