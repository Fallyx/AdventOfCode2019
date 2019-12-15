using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;

namespace AdventOfCode2019.Day13
{
    class Day13
    {
        const string inputPath = @"Day13/Input.txt";

        public static void Task1()
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();

            int pos = 0;
            long relativeBase = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                long[] intsLine = line.Split(',').Select(Int64.Parse).ToArray();

                for (int i = 0; i < intsLine.Length; i++)
                {
                    memory.Add(i, intsLine[i]);
                }
            }

            Operation(memory, 0, pos, relativeBase);
        }

        public static void Task2()
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();

            int pos = 0;
            long relativeBase = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line = reader.ReadLine();

                long[] intsLine = line.Split(',').Select(Int64.Parse).ToArray();

                for (int i = 0; i < intsLine.Length; i++)
                {
                    memory.Add(i, intsLine[i]);
                }
            }

            memory[0] = 2;

            Operation(memory, 0, pos, relativeBase, true);
        }

        private static void Operation(Dictionary<long, long> memory, long input, long pos, long relativeBase, bool displayScore = false)
        {
            Dictionary<Point, int> screen = new Dictionary<Point, int>();

            int outputOp = 0;
            int x = 0;
            int y = 0;
            long score = 0;
            Point paddle = new Point();

            while (true)
            { 
                long opcode = memory[pos];
                long param1 = ((opcode / 100) % 10);
                long param2 = ((opcode / 1000) % 10);
                long param3 = ((opcode / 10000) % 10);

                opcode %= 100;

                if (opcode == 99)
                {
                    if (displayScore) Console.WriteLine(score);
                    else Console.WriteLine(screen.Values.Count(t => t == 2));
                    break;
                }

                long val1 = GetValue(param1, memory, pos + 1, relativeBase);                

                if (opcode == 3)
                {
                    if (!memory.ContainsKey(pos + 1)) memory.Add(pos + 1, 0);

                    if (param1 == 0) memory[memory[pos + 1]] = input;
                    else
                    {
                        if (!memory.ContainsKey(relativeBase + memory[pos + 1])) memory.Add(relativeBase + memory[pos + 1], 0);
                        memory[relativeBase + memory[pos + 1]] = input;
                    }

                    pos += 2;

                    continue;
                }
                else if (opcode == 4)
                {
                    if (outputOp == 0) x = (int)val1;
                    else if (outputOp == 1) y = (int)val1;
                    else
                    {
                        if (x == -1 && y == 0) score = val1;
                        else
                        {
                            Point p = new Point(x, y);

                            if (val1 == 3) paddle = new Point(x, y);
                            else if (val1 == 4)
                            {
                                input = x - paddle.X;
                            }

                            if (screen.ContainsKey(p)) screen[p] = (int)val1;
                            else screen.Add(p, (int)val1);
                        }

                        outputOp = -1;
                    }

                    outputOp++;

                    pos += 2;

                    continue;
                }
                else if (opcode == 9)
                {
                    relativeBase += val1;
                    pos += 2;

                    continue;
                }

                long val2 = GetValue(param2, memory, pos + 2, relativeBase);

                if (opcode == 5)
                {
                    if (val1 != 0) pos = val2;
                    else pos += 3;

                    continue;
                }
                else if (opcode == 6)
                {
                    if (val1 == 0) pos = val2;
                    else pos += 3;

                    continue;
                }

                long saveLoc = SaveLocation(param3, memory, pos + 3, relativeBase);                

                if (opcode == 1)
                {
                    memory[saveLoc] = val1 + val2;

                    pos += 4;
                }
                else if (opcode == 2)
                {
                    memory[saveLoc] = val1 * val2;

                    pos += 4;
                }
                else if (opcode == 7)
                {
                    if (val1 < val2) memory[saveLoc] = 1;
                    else memory[saveLoc] = 0;

                    pos += 4;
                }
                else if (opcode == 8)
                {
                    if (val1 == val2) memory[saveLoc] = 1;
                    else memory[saveLoc] = 0;

                    pos += 4;
                }
            }
        }

        private static long GetValue(long param, Dictionary<long, long> memory, long pos, long relativeBase)
        {
            long val;
            if (!memory.ContainsKey(memory[pos])) memory.Add(memory[pos], 0);

            if (param == 0) val = memory[memory[pos]];
            else if (param == 1)
            {
                if (!memory.ContainsKey(pos)) memory.Add(pos, 0);
                val = memory[pos];
            }
            else
            {
                if (!memory.ContainsKey(relativeBase + memory[pos])) memory.Add(relativeBase + memory[pos], 0);
                val = memory[relativeBase + memory[pos]];
            }

            return val;
        }

        private static long SaveLocation(long param, Dictionary<long, long> memory, long pos, long relativeBase)
        {
            long saveLoc;

            if (!memory.ContainsKey(pos)) memory.Add(pos, 0);

            if (param == 0) saveLoc = memory[pos];
            else
            {
                if (!memory.ContainsKey(relativeBase + memory[pos])) memory.Add(relativeBase + memory[pos], 0);
                saveLoc = relativeBase + memory[pos];
            }

            return saveLoc;
        }
    }
}
