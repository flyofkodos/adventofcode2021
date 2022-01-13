$readings = Get-Content input.txt

$increases = 0
$last_depth = 32767
for ($index = 0; $index -lt $readings.count; $index++) {
    if ($readings[$index] -gt $last_depth) {
        $increases++
    }
    $last_depth = $readings[$index]
}
"Part 1 depth increases $increases times."

# Part 2
$increases = 0
$last_depth = 32767
for ($index = 0; $index -lt $readings.count - 2; $index++) {
    $window = $readings[$index] + $readings[$index + 1] + $readings[$index + 2]
    if ($window -gt $last_depth) {
        $increases++
    }
    $last_depth = $window
}
"Part 2 depth increases $increases times."