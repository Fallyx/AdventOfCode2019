using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day02
{
    class Day02
    {
        const string inputPath = @"Day02/Input.txt";
        const int output = 19690720;

        public static void Task1()
        {
            int[] ints;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                ints = line.Split(',').Select(Int32.Parse).ToArray();
            }

            ints[1] = 12;
            ints[2] = 2;

            int pos = 0;

            while(true)
            {
                int opcode = ints[pos];

                if (opcode == 99) break;

                if (opcode == 1)
                {
                    ints[ints[pos + 3]] = ints[ints[pos + 1]] + ints[ints[pos + 2]];
                }
                else if (opcode == 2)
                {
                    ints[ints[pos + 3]] = ints[ints[pos + 1]] * ints[ints[pos + 2]];
                }

                pos += 4;
            }

            Console.WriteLine(ints[0]);
        }

        public static void Task2()
        {
            int[] ints;
            int[] intsBackup;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                ints = line.Split(',').Select(Int32.Parse).ToArray();
                intsBackup = line.Split(',').Select(Int32.Parse).ToArray();
            }

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    Array.Copy(intsBackup, 0, ints, 0, intsBackup.Length);

                    ints[1] = noun;
                    ints[2] = verb;

                    int pos = 0;

                    while (true)
                    {
                        int opcode = ints[pos];

                        if (opcode == 99) break;

                        if (opcode == 1)
                        {
                            ints[ints[pos + 3]] = ints[ints[pos + 1]] + ints[ints[pos + 2]];
                        }
                        else if (opcode == 2)
                        {
                            ints[ints[pos + 3]] = ints[ints[pos + 1]] * ints[ints[pos + 2]];
                        }
                        else break;

                        pos += 4;
                    }

                    if (ints[0] == output)
                    {
                        Console.WriteLine(100 * noun + verb);
                        return;
                    }
                }
            }
        }
    }
}
