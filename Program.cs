using System.Diagnostics;

namespace AdventOfCode2024 {
    public class Program {
        private static void Main() {
            int dayNum = WithinLimit(3);
            dynamic day;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            switch (dayNum) {
                case 1:
                    day = new Day1();
                    break;
                case 2:
                    day = new Day2();
                    break;
                case 3:
                    day = new Day3();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dayNum), "Encountered a day not in the switch statement");
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            Console.WriteLine(String.Format("Parsing the input.txt file took: {0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
            day.OutputParts();
        }

        private static int WithinLimit(int end) {
            Console.WriteLine("Please enter a day (1 to {0})", end);
            int input = Convert.ToInt32(Console.ReadLine());
            if (input < 1 || input > end) throw new ArgumentOutOfRangeException(nameof(input), "Paramater cannot be less than 1 or greater than " + end);
            return input;
        }
    }
}