namespace AdventOfCode2024 {
    public class Day6 : Day<int> {
        private enum Direction {
            North = 0,
            East,
            South,
            West
        };
        private Dictionary<Direction, int[]> changes = new Dictionary<Direction, int[]>{
            { Direction.North, new int[]{ -1,  0 }},
            { Direction.East,  new int[]{  0,  1 }},
            { Direction.South, new int[]{  1,  0 }},
            { Direction.West,  new int[]{  0, -1 }}
        };

        // inputs
        private char[][] grid;
        private int startRow, startCol;
        public Day6() {
            (grid, startRow, startCol) = ReadFile();
        }
        private (char[][], int, int) ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day6.txt");
            grid = new char[lines.Length][];
            startRow = -1;
            startCol = -1;
            for (int row = 0; row < lines.Length; row++) {
                grid[row] = lines[row].ToCharArray();
                for (int col = 0; col < lines[row].Length; col++) {
                    if (lines[row][col] == '^') {
                        startRow = row;
                        startCol = col;
                    }
                }
            }
            return (grid, startRow, startCol);
        }
        public override int Part1() {
            bool[,] visited = new bool[grid.Length, grid[0].Length];

            // loop until out of bounds
            Direction dir = Direction.North;
            int posRow = startRow, posCol = startCol;
            while (posCol >= 0 && posRow >= 0 && posRow < grid.Length && posCol < grid[posRow].Length) {
                // set as visited
                visited[posRow, posCol] = true;

                // apply changes to direction and position
                (int nextRow, int nextCol) = ApplyDirectionChanges(dir, posRow, posCol);
                
                // rotate if blocked
                if (nextCol >= 0 && nextRow >= 0 && nextRow < grid.Length && nextCol < grid[nextRow].Length && grid[nextRow][nextCol] == '#') {
                    dir = Rotate90(dir);

                // otherwise move forward
                } else {
                    posCol = nextCol;
                    posRow = nextRow;
                }
            }

            // count visited positions
            int count = 0;
            for (int i = 0; i < visited.GetLength(0); i++) {
                for (int j = 0; j < visited.GetLength(1); j++) {
                    if (visited[i, j]) count++;
                }
            }
            return count;
        }
        public override int Part2() {
            int count = 0;
            bool[,,] visited = new bool[grid.Length, grid[0].Length, Enum.GetNames(typeof(Direction)).Length];
            bool[,] hadObstacle = new bool[grid.Length, grid[0].Length];

            // loop until out of bounds
            Direction dir = Direction.North;
            int posRow = startRow, posCol = startCol;
            while (posCol >= 0 && posRow >= 0 && posRow < grid.Length && posCol < grid[posRow].Length) {
                // set as visited
                visited[posRow, posCol, (int)dir] = true;

                // apply changes to position
                (int nextRow, int nextCol) = ApplyDirectionChanges(dir, posRow, posCol);
                
                // rotate if blocked
                if (nextCol >= 0 && nextRow >= 0 && nextRow < grid.Length && nextCol < grid[nextRow].Length && grid[nextRow][nextCol] == '#') {
                    dir = Rotate90(dir);
                    continue;
                }
                
                // check if rotating leads to a loop
                if (
                    nextCol >= 0 && nextRow >= 0 && nextRow < grid.Length && nextCol < grid[nextRow].Length && // check the coordinate for the obstacle is valid
                    (nextRow != startRow || nextCol != startCol) && // check the coordinate is not where the guard is currently placed
                    !hadObstacle[nextRow, nextCol] // check the obstacle has not been checked previously
                ) {
                    // set as visited
                    hadObstacle[nextRow, nextCol] = true;

                    // set up the extra blockage
                    char temp = grid[nextRow][nextCol];
                    grid[nextRow][nextCol] = '#';

                    // process if the infinite loop occurs
                    bool[,,] tempVisited = new bool[visited.GetLength(0), visited.GetLength(1), visited.GetLength(2)]; // create a copy of visited's current state to avoid changing it
                    for (int i = 0; i < visited.GetLength(0); i++) {
                        for (int j = 0; j < visited.GetLength(1); j++) {
                            for (int k = 0; k < visited.GetLength(2); k++) {
                                tempVisited[i, j, k] = visited[i, j, k];
                            }
                        }
                    }
                    if (IsInfiniteLoop(Rotate90(dir), posRow, posCol, tempVisited)) count++;

                    // backtrack extra blockage
                    grid[nextRow][nextCol] = temp;
                }

                // move forward
                posCol = nextCol;
                posRow = nextRow;
            }
            return count;
        }
        /*
            Rotates a direction 90* e.g. from North (up) to East (right)
        */
        private Direction Rotate90(Direction dir) {
            return (Direction)((Convert.ToInt32(dir) + 1) % Enum.GetNames(typeof(Direction)).Length);
        }
        /*
            Applies the relevant coordinate changes for the given direction + coordinates
        */
        private (int, int) ApplyDirectionChanges(Direction dir, int row, int col) {
            int[] change = changes[dir];
            return (row + change[0], col + change[1]);
        }
        /*
            Finds if the given coordinate + direction leads to a loop (i.e. revisiting the same position and direction)
        */
        private bool IsInfiniteLoop(Direction dir, int row, int col, bool[,,] visited) {
            // loop until out of bounds
            int posRow = row, posCol = col;
            while (posCol >= 0 && posRow >= 0 && posRow < grid.Length && posCol < grid[posRow].Length) {
                // set as visited
                if (visited[posRow, posCol, (int)dir]) return true;
                visited[posRow, posCol, (int)dir] = true;

                // apply changes to position
                (int nextRow, int nextCol) = ApplyDirectionChanges(dir, posRow, posCol);
                
                // rotate if blocked
                if (nextCol >= 0 && nextRow >= 0 && nextRow < grid.Length && nextCol < grid[nextRow].Length && grid[nextRow][nextCol] == '#') {
                    dir = Rotate90(dir);

                // otherwise move forward
                } else {
                    posCol = nextCol;
                    posRow = nextRow;
                }
            }
            return false;
        }
    }
}