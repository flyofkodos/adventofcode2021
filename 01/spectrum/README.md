# Advent of Code Day 01  

This is a Sinclair Spectrum version of the solution for the [Advent of Code 2021 day 1 puzzle](https://adventofcode.com/2021/day/1)  

To see if I can still throw together Z80A assembler code.  

## Design  

To keep code size down this uses Sinclair BASIC to print the results, which are stored in memory below the code.  

## Building and Testing  

### Tools  

* The [SjASMPlus](https://github.com/sjasmplus) assembler for no particular reason.  
* Any emulator that can load snapshots and save TZX files.  
* Screenshot of a real keyboard for reference.  

## Packaging  

1. Compile with `sjasmplus depths.zx`  
2. Load resulting snapshot into the emulator  
3. Manually enter the `depths.bas` program
4. Save with `SAVE "sonar" LINE 0` to autorun
5. Get the code size from the assembler output  
5. Save the code with `SAVE "sonar" CODE 32768,{size of code}`


## TODO  

* Fix the Part 2 code  

