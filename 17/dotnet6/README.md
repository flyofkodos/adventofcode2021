# Advent of Code Day 17  

The C# version of the solution for the [Advent of Code 2021 day 17 puzzle](https://adventofcode.com/2021/day/17)  

## Design  

### Part 1  

To find the highest `y` value we need to find the velocity needed to hit the top row of the target on the way down.  
The highest point is then the triangular number of `y`:  
$$
dy = \frac{y(y+1)}{2}
$$

The `x` velocity isn't relevant for Part 1, but can be calculated as between
$$
dx_{min} = \lfloor\sqrt{2l}]\rfloor
$$
and
$$
dx_{max} = \lfloor\sqrt{2r}\rfloor
$$
where `l` is the left extents of the target area and `r` is the right extents.  

### Part 2  

Still being worked on.  

## TODO

* Part 2 code  
