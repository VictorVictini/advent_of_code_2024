namespace AdventOfCode2024 {
    public class Day5 : Day<int> {
        Dictionary<int, HashSet<int>> parents;
        int[][] updateOrder;
        public Day5() {
            (parents, updateOrder) = ReadFile();
        }
        private (Dictionary<int, HashSet<int>>, int[][]) ReadFile() {
            string[] input = File.ReadAllText("inputs/day5.txt").Split("\n\r\n");
            string[] parentComponents = input[0].Split("\n");
            string[] pageComponents = input[1].Split("\n");

            // setting up parents hashset
            parents = new Dictionary<int, HashSet<int>>();
            foreach (string line in parentComponents) {
                string[] components = line.Split("|");
                int child = Convert.ToInt32(components[0]);
                int parent = Convert.ToInt32(components[1]);
                if (parents.ContainsKey(child)) {
                    parents[child].Add(parent);
                } else {
                    parents.Add(child, new HashSet<int>{parent});
                }
            }

            // setting up the page update orderings
            int index = 0;
            updateOrder = new int[pageComponents.Length][];
            foreach (string line in pageComponents) {
                string[] components = line.Split(",");
                updateOrder[index] = new int[components.Length];
                for (int i = 0; i < components.Length; i++) {
                    updateOrder[index][i] = Convert.ToInt32(components[i]);
                }
                index++;
            }

            return (parents, updateOrder);
        }
        public override int Part1() {
            int sum = 0;
            foreach (int[] ordering in updateOrder) {
                if (IsValidUpdateOrdering(ordering)) sum += ordering[ordering.Length / 2];
            }
            return sum;
        }
        public override int Part2() {
            int sum = 0;
            foreach (int[] ordering in updateOrder) {
                if (!IsValidUpdateOrdering(ordering)) sum += FixUpdateOrdering(ordering)[ordering.Length / 2];
            }
            return sum;
        }
        /*
            Check if the previous nodes contain the parents of the current node, and if so they're invalid
        */
        private bool IsValidUpdateOrdering(int[] ordering) {
            for (int i = 0; i < ordering.Length; i++) {
                for (int j = 0; j < i; j++) {
                    if (parents.ContainsKey(ordering[i]) && parents[ordering[i]].Contains(ordering[j])) return false;
                }
            }
            return true;
        }
        /*
            Fixes the update ordering by moving any parents forward 
        */
        private int[] FixUpdateOrdering(int[] ordering) {
            for (int i = 0; i < ordering.Length; i++) {
                for (int j = 0; j < i; j++) {
                    if (parents.ContainsKey(ordering[i]) && parents[ordering[i]].Contains(ordering[j])) {
                        // swap the values so they're in the correct order
                        int temp = ordering[i];
                        ordering[i] = ordering[j];
                        ordering[j] = temp;

                        // move indexer back by 1 (so it restarts at the swapped-in value) & end loop
                        i--;
                        break;
                    }
                }
            }
            return ordering;
        }
    }
}