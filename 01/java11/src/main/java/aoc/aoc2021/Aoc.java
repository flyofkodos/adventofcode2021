/**
 *
 */
package aoc.aoc2021;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.stream.Collectors;

public class Aoc {

	static Logger logger = Logger.getLogger(Aoc.class.getName());
	/**
	 * @param args
	 * @throws IOException
	 */
	public static void main(String[] args) throws IOException {
		
		Path inputPath = Path.of("input.txt");
		List<String> lines = Files.readAllLines(inputPath);
		List<Integer> content = lines.stream().map(Integer::parseInt).collect(Collectors.toList());
		lines.clear();
		logger.log(Level.INFO,"Part 1 is {0}", part1(content));
		logger.log(Level.INFO,"Part 2 is {0}", part2(content));
	}

	private static int increases;
	private static Integer lastDepth;

	private static int part1(List<Integer> readings) {
		increases = 0;
		lastDepth = 32767;

		readings.forEach(reading -> {
			if (reading > lastDepth) {
				increases++;
			}
			lastDepth = reading;
		});
		return increases;
	}

	private static int part2(List<Integer> readings) {
		increases = 0;
		lastDepth = 32767;
		for (int x = 0; x < readings.size() - 2; x++) {
			int window = readings.get(x) + readings.get(x + 1) + readings.get(x + 2);
			if (window > lastDepth) {
				increases++;
			}
			lastDepth = window;
		}
		return increases;
	}
}
