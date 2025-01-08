namespace AdventOfCode2024 {
    public class Day7 : Day<long> {
        private long[] answers;
        private long[][] components;
        public Day7() {
            (answers, components) = ReadFile();
        }
        private (long[], long[][]) ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day7.txt");
            answers = new long[lines.Length];
            components = new long[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                string[] parts = lines[i].Split(": ");
                answers[i] = Convert.ToInt64(parts[0]);
                string[] equationParts = parts[1].Split(" ");
                components[i] = new long[equationParts.Length];
                for (int j = 0; j < equationParts.Length; j++) {
                    components[i][j] = Convert.ToInt64(equationParts[j]);
                }
            }
            return (answers, components);
        }
        public override long Part1() {
            long sum = 0;
            for (int i = 0; i < answers.Length; i++) {
                if (HasExpectedResultPart1(components[i], answers[i])) sum += answers[i];
            }
            return sum;
        }
        public override long Part2() {
            long sum = 0;
            for (int i = 0; i < answers.Length; i++) {
                if (HasExpectedResultPart2(components[i], answers[i])) sum += answers[i];
            }
            return sum;
        }
        /*
            Determines if the given list of numbers can result in the expected answer with multiplication and addition
        */
        private bool HasExpectedResultPart1(long[] list, long answer) {
            // fill in the first value
            HashSet<long>[] results = new HashSet<long>[list.Length];
            results[0] = new HashSet<long>{list[0]};

            // fill in the rest of the values
            for (int i = 1; i < list.Length; i++) {
                results[i] = new HashSet<long>();
                foreach (long num in results[i - 1]) {
                    long val = num + list[i];
                    if (val <= answer) results[i].Add(val);
                    val = num * list[i];
                    if (val <= answer) results[i].Add(val);
                }
            }

            // check the last hashset contains the expected result
            return results[^1].Contains(answer);
        }
        /*
            Determines if the given list of numbers can result in the expected answer with multiplication, concatenation and addition
        */
        private bool HasExpectedResultPart2(long[] list, long answer) {
            // fill in the first value
            HashSet<long>[] results = new HashSet<long>[list.Length];
            results[0] = new HashSet<long>{list[0]};

            // fill in the rest of the values
            for (int i = 1; i < list.Length; i++) {
                results[i] = new HashSet<long>();
                foreach (long num in results[i - 1]) {
                    long val = num + list[i];
                    if (val <= answer) results[i].Add(val);
                    val = num * list[i];
                    if (val <= answer) results[i].Add(val);
                    val = Convert.ToInt64(Convert.ToString(num) + Convert.ToString(list[i]));
                    if (val <= answer) results[i].Add(val);
                }
            }

            // check the last hashset contains the expected result
            return results[^1].Contains(answer);
        }
    }
}