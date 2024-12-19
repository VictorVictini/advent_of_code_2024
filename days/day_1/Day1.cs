namespace AdventOfCode2024 {
    public class Day1 : Day<int> {
        private int[] first, second;
        public Day1() {
            (first, second) = ReadFile();
        }
        private (int[], int[]) ReadFile() {
            string[] lines = File.ReadAllLines("days/day_1/input.txt");
            first = new int[lines.Length];
            second = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                string[] components = lines[i].Split(" ");
                first[i] = Convert.ToInt32(components[0]);
                second[i] = Convert.ToInt32(components[^1]);
            }
            return (first, second);
        }
        public override int Part1() {
            // sort so the smallest appear first
            Array.Sort(first);
            Array.Sort(second);

            // find sum of differences
            int res = 0;
            for (int i = 0; i < first.Length; i++) {
                res += Math.Abs(first[i] - second[i]);
            }
            return res;
        }
        public override int Part2() {
            // create a counter map over the 2nd list
            Dictionary<int, int> counter = new Dictionary<int, int>();
            foreach (int num in second) {
                if (counter.ContainsKey(num)) {
                    counter[num]++;
                } else {
                    counter[num] = 1;
                }
            }

            // calculate the similarity score
            int score = 0;
            foreach (int num in first) {
                if (counter.ContainsKey(num)) score += counter[num] * num;
            }
            return score;
        }
    }
}