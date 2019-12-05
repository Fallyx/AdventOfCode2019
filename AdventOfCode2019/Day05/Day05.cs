using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day05
{
    class Day05
    {
        const string inputPath = @"Day05/Input.txt";
        const int input = 1;

        public static void Task1()
        {
            int[] ints;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                ints = line.Split(',').Select(Int32.Parse).ToArray();
            }

            int pos = 0;

            while (true)
            {
                int opcode = ints[pos];
                int[] opParams;
                int param1 = 0;
                int param2 = 0;

                int digits = (int)(Math.Log10(Math.Abs(opcode)) + 1);

                if (digits == 5)
                {
                    opParams = opcode.ToString().Substring(1, 2).Select(c => (int)Char.GetNumericValue(c)).ToArray();

                    param1 = opParams[1];
                    param2 = opParams[0];
                }
                if (digits == 4)
                {
                    opParams = opcode.ToString().Substring(0, 2).Select(c => (int)Char.GetNumericValue(c)).ToArray();

                    param1 = opParams[1];
                    param2 = opParams[0];
                }
                else if (digits == 3)
                {
                    opParams = opcode.ToString().Substring(0, 1).Select(c => (int)Char.GetNumericValue(c)).ToArray();

                    param1 = opParams[0];
                }

                opcode %= 100;

                if (opcode == 99)
                {
                    break;
                }
                else if (opcode == 1)
                {
                    int val1 = (param1 == 1) ? ints[pos + 1] : ints[ints[pos + 1]];
                    int val2 = (param2 == 1) ? ints[pos + 2] : ints[ints[pos + 2]];

                    ints[ints[pos + 3]] = val1 + val2;

                    pos += 4;
                }
                else if (opcode == 2)
                {
                    int val1 = (param1 == 1) ? ints[pos + 1] : ints[ints[pos + 1]];
                    int val2 = (param2 == 1) ? ints[pos + 2] : ints[ints[pos + 2]];

                    ints[ints[pos + 3]] = val1 * val2;

                    pos += 4;
                }
                else if (opcode == 3)
                {
                    ints[ints[pos + 1]] = input;

                    pos += 2;
                }
                else if (opcode == 4)
                {
                    Console.WriteLine(ints[ints[pos + 1]]);

                    pos += 2;
                }
            }
        }
    }
}
