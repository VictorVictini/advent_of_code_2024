namespace AdventOfCode2024 {
    public class Day8 : Day<int> {
        private Dictionary<char, List<(int, int)>> node_groups;
        private int length, height;
        public Day8() {
            (node_groups, length, height) = ReadFile();
        }
        private (Dictionary<char, List<(int, int)>>, int, int) ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day8.txt");
            height = lines.Length;
            length = lines[0].Length;
            node_groups = new Dictionary<char, List<(int, int)>>();
            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[i].Length; j++) {
                    if (lines[i][j] == '.') continue;
                    if (node_groups.ContainsKey(lines[i][j])) {
                        node_groups[lines[i][j]].Add((i, j));
                    } else {
                        node_groups.Add(lines[i][j], new List<(int, int)>{(i, j)});
                    }
                }
            }
            return (node_groups, length, height);
        }
        public override int Part1() {
            // stores locations of antinodes uniquely
            HashSet<(int, int)> antinodes = new HashSet<(int, int)>();

            // get groups to loop over each other and find every combination that has an antinode
            foreach (KeyValuePair<char, List<(int, int)>> pair in node_groups) {
                foreach ((int i, int j) in pair.Value) {
                    foreach ((int k, int l) in pair.Value) {
                        if (i == k && j == l) continue;
                        int diff_x = l - j;
                        int diff_y = k - i;
                        int x = j - diff_x;
                        int y = i - diff_y;
                        if (x < 0 || y < 0 || x >= length || y >= height) continue;
                        antinodes.Add((x, y));
                    }
                }
            }
            return antinodes.Count;
        }
        public override int Part2() {
            // stores locations of antinodes uniquely
            HashSet<(int, int)> antinodes = new HashSet<(int, int)>();

            // get groups to loop over each other and find every combination that has an antinode
            foreach (KeyValuePair<char, List<(int, int)>> pair in node_groups) {
                foreach ((int i, int j) in pair.Value) {
                    foreach ((int k, int l) in pair.Value) {
                        if (i == k && j == l) continue;
                        int diff_x = l - j;
                        int diff_y = k - i;

                        // create antinodes until we reach the grid limits
                        for (int x = j, y = i; x >= 0 && y >= 0 && x < length && y < height; x -= diff_x, y -= diff_y) {
                            antinodes.Add((x, y));
                        }
                    }
                }
            }
            return antinodes.Count;
        }
    }
}