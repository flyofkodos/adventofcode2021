import unittest
from binary import part1, part2


class Test_day02_test(unittest.TestCase):

    def test_part1(self):
        test_data = [0b00100, 0b11110, 0b10110, 0b10111, 0b10101,
                     0b01111, 0b00111, 0b11100, 0b10000, 0b11001, 0b00010, 0b01010]
        self.assertEqual(198, part1(test_data))

    def test_part2(self):
        test_data = [0b00100, 0b11110, 0b10110, 0b10111, 0b10101,
                     0b01111, 0b00111, 0b11100, 0b10000, 0b11001, 0b00010, 0b01010]
        self.assertEqual(230, part2(test_data))


if __name__ == '__main__':
    unittest.main()
