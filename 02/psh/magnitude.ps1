$commands = Get-Content $PSScriptRoot\input.txt

$horiz = 0
$depth = 0

ForEach ($command in $commands) {
    $parts = $command.Split(' ')
    $magnitude = $parts[1]
    switch ($parts[0]) {
        "forward" { $horiz += $magnitude }
        "backward" { $horiz -= $magnitude }
        "down" { $depth += $magnitude }
        "up" { $depth -= $magnitude }
        Default {}
    }
}
"Part 1 depth $depth position $horiz - $($horiz * $depth)"

$horiz = 0
$depth = 0
$aim = 0
ForEach ($command in $commands) {
    $parts = $command.Split(' ')
    $magnitude = $parts[1]
    switch ($parts[0]) {
        "forward" { $horiz += $magnitude ; $depth += ($aim * $magnitude) }
        "backward" { $horiz -= $magnitude; $depth -= ($aim * $magnitude) }
        "down" { $aim += $magnitude }
        "up" { $aim -= $magnitude }
        Default {}
    }
}
"Part 2 depth $depth position $horiz - $($horiz * $depth)"