# Advent of Code Day 17  

The C# version of the solution for the [Advent of Code 2021 day 17 puzzle](https://adventofcode.com/2021/day/17)  

## Design  

The design assumes that the target area is entirely below and to the right of the start point.  
### Part 1  

To find the highest `y` value we need to find the velocity needed to hit the bottom row of the target on the way down.  
This handily equates to the difference between the start height and bottom row `b` minus 1  
`dy = (y`<sub>`start`</sub>` - b) - 1`

The highest point is then the triangular number of `y`:  
`y`<sub>`max`</sub>` = (y(y + 1)) / 2`

The `x` velocity isn't relevant for Part 1, but can be calculated as between `dx`<sub>`min`</sub>` = floor(sqrt(2l))` and `dx`<sub>`min`</sub>` = floor(sqrt(22))` where `l` is the left extents of the target area and `r` is the right extents.  

### Part 2  

There is probably a mathematical way to work this out, but I've gone for the brute-foce approach.  

The `x` velocity limits are calculated as between `dx`<sub>`min`</sub>` = floor(sqrt(2l))` and `dx`<sub>`max`</sub>` = r` where `l` is the left extents of the target area and `r` is the right extents.  
The `y` velocity limits are calculated as between `y`<sub>`min`</sub>` = b` and `y`<sub>`max`</sub>` = (y(y + 1)) / 2`  
The code loops through each possible `x` and `y` velocity and counts which hit the target.  