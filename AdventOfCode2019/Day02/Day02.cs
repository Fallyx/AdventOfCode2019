using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day02
{
    class Day02
    {
        const string inputPath = @"Day02/Input.txt";

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

                if (opcode == 99)
                {
                    break;
                }

                int pos1 = pos + 1;
                int pos2 = pos + 2;
                int pos3 = pos + 3;

                if (opcode == 1)
                {
                    ints[ints[pos3]] = ints[ints[pos1]] + ints[ints[pos2]];
                }
                else if (opcode == 2)
                {
                    ints[ints[pos3]] = ints[ints[pos1]] * ints[ints[pos2]];
                }

                pos += 4;
            }

            Console.WriteLine(ints[0]);
        }
    }
}
