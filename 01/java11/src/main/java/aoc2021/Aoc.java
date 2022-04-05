/**
 *
 */
package aoc2021;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.stream.Collectors;

public class Aoc {

	/**
	 * @param args
	 * @throws IOException
	 */
	public static void main(String[] args) throws IOException {
		Path inputPath = Path.of("input.txt");
		List<String> lines = Files.readAllLines(inputPath);
		List<Integer> content = lines.stream().map(Integer::parseInt).collect(Collectors.toList());
		lines.clear();
		System.out.println("Part 1 is " + part1(content));
		System.out.println("Part 2 is " + part2(content));
	}

	static private int increases;
	private static Integer last_depth;

	private static int part1(List<Integer> readings) {
		increases = 0;
		last_depth = 32767;

		readings.forEach((reading) -> {
			if (reading > last_depth) {
				increases++;
			}
			last_depth = reading;
		});
		return increases;
	}

	private static int part2(List<Integer> readings) {
		increases = 0;
		last_depth = 32767;
		for (int x = 0; x < readings.size() - 2; x++) {
			int window = readings.get(x) + readings.get(x + 1) + readings.get(x + 2);
			if (window > last_depth) {
				increases++;
			}
			last_depth = window;
		}
		return increases;
	}
}
