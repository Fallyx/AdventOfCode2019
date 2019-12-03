using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace AdventOfCode2019.Day03
{
    class Day03
    {
        static int shortestDistance = int.MaxValue;
        static int shortestSteps = int.MaxValue;
        static int step = 0;

        public static void Task1and2()
        {
            const string inputPath = @"Day03/Input.txt";

            Dictionary<Point, (bool first, int stepsF, bool second, int stepsS)> coord = new Dictionary<Point, (bool first, int stepsF, bool second, int stepsS)>();

            bool first = false;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while((line = reader.ReadLine()) != null)
                {
                    string[] moves = line.Split(',');

                    Point xy = new Point(0, 0);
                    first = !first;
                    step = 0;

                    for (int i = 0; i < moves.Length; i++)
                    {
                        xy = CableCoord(coord, xy, moves[i], first);
                    }
                }
            }

            Console.WriteLine(shortestDistance);
            Console.WriteLine(shortestSteps);
        }

        private static Point CableCoord(Dictionary<Point, (bool first, int stepsF, bool second, int stepsS)> coord, Point xy, string move, bool firstCable)
        {
            int moveInt = int.Parse(move.Substring(1));
            char direction = move[0];

            for (int i = 1; i <= moveInt; i++)
            {
                step++;

                if (direction == 'U')
                {
                    xy = new Point(xy.X, xy.Y + 1);
                }
                else if (direction == 'D')
                {
                    xy = new Point(xy.X, xy.Y - 1);
                }
                else if (direction == 'L')
                {
                    xy = new Point(xy.X - 1, xy.Y);
                }
                else if (direction == 'R')
                {
                    xy = new Point(xy.X + 1, xy.Y);
                }

                if (!coord.ContainsKey(xy)) coord.Add(xy, (false, 0, false, 0));

                if (firstCable)
                {
                    coord[xy] = (true, step, coord[xy].second, coord[xy].stepsS);
                }
                else
                {
                    coord[xy] = (coord[xy].first, coord[xy].stepsF, true, step);

                    if (coord[xy].first && coord[xy].second)
                    {
                        shortestDistance = Math.Min(shortestDistance, Math.Abs(xy.X) + Math.Abs(xy.Y));

                        if (coord[xy].stepsF != 0)
                        {
                            shortestSteps = Math.Min(shortestSteps, coord[xy].stepsF + coord[xy].stepsS);
                        }
                    }
                }
                    
            }

            return xy;
        }
    }
}
