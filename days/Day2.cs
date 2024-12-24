using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace AdventOfCode2024 {
    public class Day2 : Day<int> {
        private int min_diff = 1;
        private int max_diff = 3;
        private int[][] reports;
        public Day2() {
            reports = ReadFile();
        }
        private int[][] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day2.txt");
            reports = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                string[] components = lines[i].Split(" ");
                reports[i] = new int[components.Length];
                for (int j = 0; j < components.Length; j++) {
                    reports[i][j] = Convert.ToInt32(components[j]);
                }
            }
            return reports;
        }
        public override int Part1() {
            int score = 0;
            foreach (int[] report in reports) {
                if (!FindBadLevelExists(report)) score++;
            }
            return score;
        }
        public override int Part2() {
            int score = 0;
            foreach (int[] report in reports) {
                // get the current bad level if any
                if (!FindBadLevelExists(report)) {
                    score++;
                    continue;
                }

                // brute force forgiving every level
                int[] temp = new int[report.Length - 1];
                for (int j = 0; j < report.Length; j++) {
                    // create array excluding one level
                    for (int k = 0, l = 0; k < report.Length; k++) {
                        if (k != j) temp[l++] = report[k];
                    }

                    // find if it's valid
                    if (!FindBadLevelExists(temp)) {
                        score++;
                        break;
                    }
                }
            }
            return score;
        }
        /*
            Finds if a bad level exists
        */
        private bool FindBadLevelExists(int[] report) {
            if (report.Length <= 1) return false;

            // multiplier for incrementing or decrementing
            int incr = 1;
            if (report[1] < report[0]) incr = -1;

            // checking validity of report
            for (int i = 1; i < report.Length; i++) {
                int diff = report[i] - report[i - 1];
                int diff_abs = Math.Abs(diff);

                if (diff_abs == 0 || diff_abs < min_diff) return true;
                if (diff_abs > max_diff || diff / diff_abs != incr) return true;
            }
            return false;
        }
    }
}