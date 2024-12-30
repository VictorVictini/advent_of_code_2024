namespace AdventOfCode2024 {
    public class Day4 : Day<int> {
        // for part 1
        string wordPart1 = "XMAS";
        int[][] changes = new int[][]{
            // normal directions
            new int[]{0, 1},
            new int[]{1, 0},
            new int[]{0, -1},
            new int[]{-1, 0},

            // diagonals
            new int[]{1, 1},
            new int[]{-1, -1},
            new int[]{1, -1},
            new int[]{-1, 1}
        };

        // for part 2
        string wordPart2 = "MAS";
        int[][] surrounding = new int[][]{
            new int[]{1, 1},
            new int[]{-1, -1},
            new int[]{1, -1},
            new int[]{-1, 1}
        };
        string[] wordSearch;
        public Day4() {
            wordSearch = ReadFile();
        }
        private string[] ReadFile() {
            return File.ReadAllLines("inputs/day4.txt");
        }
        public override int Part1() {
            int count = 0;
            for (int i = 0; i < wordSearch.Length; i++) {
                for (int j = 0; j < wordSearch[i].Length; j++) {
                    count += CountWordDirections(i, j);
                }
            }
            return count;
        }
        public override int Part2() {
            int count = 0;
            for (int i = 0; i < wordSearch.Length; i++) {
                for (int j = 0; j < wordSearch[i].Length; j++) {
                    if (IsXWord(i, j)) count++;
                }
            }
            return count;
        }
        /*
            Check every direction for if the expected word occurs
        */
        private int CountWordDirections(int row, int col) {
            int count = 0;
            foreach (int[] change in changes) {
                bool found = true;
                for (int i = 0; i < wordPart1.Length; i++) {
                    int currRow = row + i * change[0];
                    int currCol = col + i * change[1];
                    if (currRow < 0 || currCol < 0 || currRow >= wordSearch.Length || currCol >= wordSearch[0].Length || wordSearch[currRow][currCol] != wordPart1[i]) {
                        found = false;
                        break;
                    }
                }
                if (found) count++;
            }
            return count;
        }
        /*
            Check if the word surrounds the point provided in an 'X' manner
        */
        private bool IsXWord(int row, int col) {
            int count = 0;
            foreach (int[] change in surrounding) {
                int quantity = wordPart2.Length / 2;
                int startRow = row - change[0] * quantity;
                int startCol = col - change[1] * quantity;
                bool found = true;
                for (int i = 0; i < wordPart2.Length; i++) {
                    int currRow = startRow + i * change[0];
                    int currCol = startCol + i * change[1];
                    if (currRow < 0 || currCol < 0 || currRow >= wordSearch.Length || currCol >= wordSearch[0].Length || wordSearch[currRow][currCol] != wordPart2[i]) {
                        found = false;
                        break;
                    }
                }
                if (found) count++;
            }
            return count == 2;
        }
    }
}
