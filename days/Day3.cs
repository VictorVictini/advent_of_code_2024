using System.Text.RegularExpressions;

namespace AdventOfCode2024 {
    public class Day3 : Day<int> {
        public enum Instruction {
            read_status,
            value
        }
        private struct Step {
            public readonly Instruction instruction;
            public readonly int value; // for boolean, 0 is false, everything else is true
            public Step(Instruction instruction, int value) {
                this.instruction = instruction;
                this.value = value;
            }
        }
        private Step[] steps;
        public Day3() {
            steps = ReadFile();
        }
        private Step[] ReadFile() {
            string input = File.ReadAllText("inputs/day3.txt");
            
            // apply a regex to get all the necessary components
            // then convert it into a more usable data structure
            Regex regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do(n't)?\(\)");
            MatchCollection matches = regex.Matches(input);
            steps = new Step[matches.Count];
            int index = 0;
            foreach (Match match in matches) {
                GroupCollection group = match.Groups;

                // parsing mul(XXX,XXX)
                if (group[1].Value.Length > 0) {
                    steps[index] = new Step(Instruction.value, Convert.ToInt32(group[1].Value) * Convert.ToInt32(group[2].Value));

                // parsing do() or don't()
                } else {
                    steps[index] = new Step(Instruction.read_status, group[3].Value.Length == 0 ? 1 : 0);
                }

                index++;
            }
            return steps;
        }
        public override int Part1() {
            int sum = 0;
            foreach (Step step in steps) {
                if (step.instruction == Instruction.value) sum += step.value;
            }
            return sum;
        }
        public override int Part2() {
            int sum = 0;
            bool readEnabled = true;
            foreach (Step step in steps) {
                switch (step.instruction) {
                    case Instruction.read_status:
                        readEnabled = step.value > 0;
                        Console.WriteLine(readEnabled);
                        break;
                    case Instruction.value:
                        if (readEnabled) sum += step.value;
                        break;
                    default:
                        throw new Exception("Day 3 Part 2: Unexpected Instruction enum value encountered");
                }
            }
            return sum;
        }
    }
}