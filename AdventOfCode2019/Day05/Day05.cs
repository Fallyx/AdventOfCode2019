using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day05
{
    class Day05
    {
        const string inputPath = @"Day05/Input.txt";
        const int inputTask1 = 1;
        const int inputTask2 = 5;

        public static void Task1()
        {
            Operation(inputTask1);
        }

        public static void Task2()
        {
            Operation(inputTask2);
        }

        private static void Operation(int input)
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
                int[] opParams = GetParams(opcode);
                int param1 = opParams[1];
                int param2 = opParams[0];

                opcode %= 100;

                if (opcode == 99)
                {
                    break;
                }
                else if (opcode == 3)
                {
                    ints[ints[pos + 1]] = input;

                    pos += 2;
                    continue;
                }
                else if (opcode == 4)
                {
                    Console.WriteLine(ints[ints[pos + 1]]);

                    pos += 2;
                    continue;
                }

                int val1 = (param1 == 1) ? ints[pos + 1] : ints[ints[pos + 1]];
                int val2 = (param2 == 1) ? ints[pos + 2] : ints[ints[pos + 2]];
                
                if (opcode == 1)
                {
                    ints[ints[pos + 3]] = val1 + val2;

                    pos += 4;
                }
                else if (opcode == 2)
                {
                    ints[ints[pos + 3]] = val1 * val2;

                    pos += 4;
                }
                else if (opcode == 5)
                {
                    if (val1 != 0) pos = val2;
                    else pos += 3;
                }
                else if (opcode == 6)
                {
                    if (val1 == 0) pos = val2;
                    else pos += 3;
                }
                else if (opcode == 7)
                {
                    if (val1 < val2) ints[ints[pos + 3]] = 1;
                    else ints[ints[pos + 3]] = 0;

                    pos += 4;
                }
                else if (opcode == 8)
                {
                    if (val1 == val2) ints[ints[pos + 3]] = 1;
                    else ints[ints[pos + 3]] = 0;

                    pos += 4;
                }
            }
        }

        private static int[] GetParams(int opcode)
        {
            int digits = (int)(Math.Log10(Math.Abs(opcode)) + 1);
            List<int> opParams;

            if (digits == 5)
            {
                opParams = opcode.ToString().Substring(1, 2).Select(c => (int)Char.GetNumericValue(c)).ToList();
            }
            if (digits == 4)
            {
                opParams = opcode.ToString().Substring(0, 2).Select(c => (int)Char.GetNumericValue(c)).ToList();
            }
            else if (digits == 3)
            {
                opParams = opcode.ToString().Substring(0, 1).Select(c => (int)Char.GetNumericValue(c)).ToList();

                opParams.Insert(0, 0);
            }
            else
            {
                opParams = new List<int> { 0, 0 };
            }

            return opParams.ToArray();
        }
    }
}
