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


        private static Point CableCoord (Dictionary<Point, (bool first, int stepsF, bool second, int stepsS)> coord, Point xy, string move, bool firstCable)
        {
            int moveInt = int.Parse(move.Substring(1));

            if (move[0] == 'U')
            {
                for (int i = 1; i <= moveInt; i++)
                {
                    step++;

                    Point p = new Point(xy.X, xy.Y + i);
                    if (!coord.ContainsKey(p)) coord.Add(p, (false, 0, false, 0));

                    if (firstCable)
                    {
                        coord[p] = (true, step, coord[p].second, coord[p].stepsS);
                    }
                    else
                    {
                        coord[p] = (coord[p].first, coord[p].stepsF, true, step);

                        if (coord[p].first  && coord[p].second)
                        {
                            shortestDistance = Math.Min(shortestDistance, Math.Abs(p.X) + Math.Abs(p.Y));

                            if (coord[p].stepsF != 0)
                            {
                                shortestSteps = Math.Min(shortestSteps, coord[p].stepsF + coord[p].stepsS);
                            }
                        }
                    }
                }

                xy = new Point(xy.X, xy.Y + moveInt);
            }
            else if (move[0] == 'D')
            {
                for (int i = 1; i <= moveInt; i++)
                {
                    step++;

                    Point p = new Point(xy.X, xy.Y - i);
                    if (!coord.ContainsKey(p)) coord.Add(p, (false, 0, false, 0));

                    if (firstCable)
                    {
                        coord[p] = (true, step, coord[p].second, coord[p].stepsS);
                    }
                    else
                    {
                        coord[p] = (coord[p].first, coord[p].stepsF, true, step);

                        if (coord[p].first && coord[p].second)
                        {
                            shortestDistance = Math.Min(shortestDistance, Math.Abs(p.X) + Math.Abs(p.Y));

                            if (coord[p].stepsF != 0)
                            {
                                shortestSteps = Math.Min(shortestSteps, coord[p].stepsF + coord[p].stepsS);
                            }
                        }
                    }
                }

                xy = new Point(xy.X, xy.Y - moveInt);
            }
            else if (move[0] == 'L')
            {
                for (int i = 1; i <= moveInt; i++)
                {
                    step++;

                    Point p = new Point(xy.X - i, xy.Y);
                    if (!coord.ContainsKey(p)) coord.Add(p, (false, 0, false, 0));

                    if (firstCable)
                    {
                        coord[p] = (true, step, coord[p].second, coord[p].stepsS);
                    }
                    else
                    {
                        coord[p] = (coord[p].first, coord[p].stepsF, true, step);

                        if (coord[p].first && coord[p].second)
                        {
                            shortestDistance = Math.Min(shortestDistance, Math.Abs(p.X) + Math.Abs(p.Y));

                            if (coord[p].stepsF != 0)
                            {
                                shortestSteps = Math.Min(shortestSteps, coord[p].stepsF + coord[p].stepsS);
                            }
                        }
                    }
                }

                xy = new Point(xy.X - moveInt, xy.Y);
            }
            else if (move[0] == 'R')
            {
                for (int i = 1; i <= moveInt; i++)
                {
                    step++;

                    Point p = new Point(xy.X + i, xy.Y);
                    if (!coord.ContainsKey(p)) coord.Add(p, (false, 0, false, 0));

                    if (firstCable)
                    {
                        coord[p] = (true, step, coord[p].second, coord[p].stepsS);
                    }
                    else
                    {
                        coord[p] = (coord[p].first, coord[p].stepsF, true, step);

                        if (coord[p].first && coord[p].second)
                        {
                            shortestDistance = Math.Min(shortestDistance, Math.Abs(p.X) + Math.Abs(p.Y));

                            if (coord[p].stepsF != 0)
                            {
                                shortestSteps = Math.Min(shortestSteps, coord[p].stepsF + coord[p].stepsS);
                            }
                        }
                    }
                }

                xy = new Point(xy.X + moveInt, xy.Y);
            }

            return xy;
        }
    }
}
