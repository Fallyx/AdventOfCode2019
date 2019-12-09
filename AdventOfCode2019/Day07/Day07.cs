using System;
using System.IO;
using System.Linq;
using Combinatorics.Collections;

namespace AdventOfCode2019.Day07
{
    class Day07
    {
        const string inputPath = @"Day07/Input.txt";

        public static void Task1()
        {
            var perms = new Permutations<int>(new int[] { 0, 1, 2, 3, 4 }, GenerateOption.WithoutRepetition);
            int biggestOutput = int.MinValue;
            int[] ints;


            foreach (var phase in perms)
            {
                int output = 0;
                foreach (var num in phase)
                {
                    using (StreamReader reader = new StreamReader(inputPath))
                    {
                        string line = reader.ReadLine();

                        ints = line.Split(',').Select(Int32.Parse).ToArray();
                    }

                    (output, _, _) = Operation(ints, num, output, true);
                }

                if (biggestOutput < output)
                {
                    biggestOutput = output;
                }
            }

            Console.WriteLine(biggestOutput);
        }

        public static void Task2()
        {
            var perms = new Permutations<int>(new int[] { 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition);
            int biggestOutput = int.MinValue;

            int[] intsBackup;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                intsBackup = line.Split(',').Select(Int32.Parse).ToArray();
            }

            foreach (var phase in perms)
            {
                bool printInp = true;

                (int[] ints, int pos, int output, bool halted) ampA = (new int[intsBackup.Length], 0, 0, false);
                (int[] ints, int pos, int output, bool halted) ampB = (new int[intsBackup.Length], 0, 0, false);
                (int[] ints, int pos, int output, bool halted) ampC = (new int[intsBackup.Length], 0, 0, false);
                (int[] ints, int pos, int output, bool halted) ampD = (new int[intsBackup.Length], 0, 0, false);
                (int[] ints, int pos, int output, bool halted) ampE = (new int[intsBackup.Length], 0, 0, false);

                Array.Copy(intsBackup, ampA.ints, ampA.ints.Length);
                Array.Copy(intsBackup, ampB.ints, ampB.ints.Length);
                Array.Copy(intsBackup, ampC.ints, ampC.ints.Length);
                Array.Copy(intsBackup, ampD.ints, ampD.ints.Length);
                Array.Copy(intsBackup, ampE.ints, ampE.ints.Length);

                while(!ampE.halted)
                {
                    (ampA.output, ampA.pos, ampA.halted) = Operation(ampA.ints, phase[0], ampE.output, printInp, ampA.pos);
                    (ampB.output, ampB.pos, ampB.halted) = Operation(ampB.ints, phase[1], ampA.output, printInp, ampB.pos);
                    (ampC.output, ampC.pos, ampC.halted) = Operation(ampC.ints, phase[2], ampB.output, printInp, ampC.pos);
                    (ampD.output, ampD.pos, ampD.halted) = Operation(ampD.ints, phase[3], ampC.output, printInp, ampD.pos);
                    (ampE.output, ampE.pos, ampE.halted) = Operation(ampE.ints, phase[4], ampD.output, printInp, ampE.pos);
                    
                    printInp = false;
                }

                if (biggestOutput < ampE.output)
                {
                    biggestOutput = ampE.output;
                }
            }

            Console.WriteLine(biggestOutput);
        }

        private static (int output, int pos, bool halted) Operation(int[] ints, int input, int output, bool printInp, int pos = 0)
        {
            while (true)
            {
                int opcode = ints[pos];
                int param1 = ((opcode / 100) % 10);
                int param2 = ((opcode / 1000) % 10);

                opcode %= 100;

                if (opcode == 99)
                {
                    break;
                }

                int val1 = (param1 == 1) ? ints[pos + 1] : ints[ints[pos + 1]];

                if (opcode == 3)
                {
                    if (printInp)
                    {
                        ints[ints[pos + 1]] = input;
                        printInp = false;
                    }
                    else
                    {
                        ints[ints[pos + 1]] = output;
                    }

                    pos += 2;

                    continue;
                }
                else if (opcode == 4)
                {
                    output = val1;

                    pos += 2;
                    return (output, pos, false);
                }

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

            return (output, pos, true);
        }
    }
}
