# Advent of Code Day 21  

The C# version of the solution for the [Advent of Code 2021 day 24 puzzle](https://adventofcode.com/2021/day/24)  

## Design  

The algorithm for serial numbers was reversed (eventually) as :  

| Digit | Min | Max | Checks       |
| ----- | --- | --- | ------------ |
| 1     | 7   | 9   | digit 14 + 6 |
| 2     | 1   | 1   | digit 13 - 8 |
| 3     | 1   | 2   | digit 12 - 7 |
| 4     | 3   | 9   | digit 5 + 2  |
| 5     | 1   | 7   | digit 4 - 2  |
| 6     | 1   | 3   | digit 11 - 6 |
| 7     | 5   | 9   | digit 8 + 4  |
| 8     | 1   | 5   | digit 7 - 4  |
| 9     | 9   | 9   | digit 10 + 8 |
| 10    | 1   | 1   | digit 9 - 8  |
| 11    | 7   | 9   | digit 6 + 6  |
| 12    | 8   | 9   | digit 3 + 7  |
| 13    | 9   | 9   | digit 2 + 8  |
| 14    | 1   | 3   | digit 1 - 6  |

## Part 1  

To find the highest possible serial, build from the maximum values for each digit (i.e. 91297395919993).  

## Part 2  

To find the lowest possible serial, build from the minimum values for each digit (i.e. 71131151917891).  
