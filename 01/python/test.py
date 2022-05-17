import unittest
from sonar import part1, part2


class day02_test(unittest.TestCase):

    def test_part1(self):
        test_data = [199, 200, 208, 210, 200, 207, 240, 269, 260, 263]
        self.assertEqual(7, part1(test_data))

    def test_part2(self):
        test_data = [199, 200, 208, 210, 200, 207, 240, 269, 260, 263]
        self.assertEqual(5, part2(test_data))


if __name__ == '__main__':
    unittest.main()
