using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace AdventOfCode2019.Day12
{
    class Day12
    {
        public static void Task1()
        {
            int steps = 1000;

            List<(Vector3 moonPos, Vector3 moonVel)> moons = new List<(Vector3 moonPos, Vector3 moonVel)>
            {
                { (new Vector3(-4, -14, 8), new Vector3(0)) },
                { (new Vector3(1, -8, 10), new Vector3(0)) },
                { (new Vector3(-15, 2, 1), new Vector3(0)) },
                { (new Vector3(-17, -17, 16), new Vector3(0)) }
            };

            Vector3[] newVels = new Vector3[moons.Count];

            for (int i = 0; i < steps; i++)
            {
                for (int first = 0; first < moons.Count; first++)
                {
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    for (int second = 0; second < moons.Count; second++)
                    {
                        if (first == second) continue;

                        x += (moons[first].moonPos.X > moons[second].moonPos.X) ? -1 : (moons[first].moonPos.X < moons[second].moonPos.X) ? 1 : 0;
                        y += (moons[first].moonPos.Y > moons[second].moonPos.Y) ? -1 : (moons[first].moonPos.Y < moons[second].moonPos.Y) ? 1 : 0;
                        z += (moons[first].moonPos.Z > moons[second].moonPos.Z) ? -1 : (moons[first].moonPos.Z < moons[second].moonPos.Z) ? 1 : 0;
                    }

                    Vector3 xyz = new Vector3(x, y, z);
                    newVels[first] = xyz;
                }

                for(int first = 0; first < moons.Count; first++)
                {
                    Vector3 newVel = moons[first].moonVel + newVels[first];
                    Vector3 newPos = moons[first].moonPos + newVel;

                    moons[first] = (newPos, newVel);
                }
            }

            int totEnergy = 0;

            foreach(var (moonPos, moonVel) in moons)
            {
                totEnergy += (int)((Math.Abs(moonPos.X) + Math.Abs(moonPos.Y) + Math.Abs(moonPos.Z)) * (Math.Abs(moonVel.X) + Math.Abs(moonVel.Y) + Math.Abs(moonVel.Z)));
            }

            Console.WriteLine(totEnergy);
        }

        public static void Task2()
        {
            List<(Vector3 moonPos, Vector3 moonVel)> moons = new List<(Vector3 moonPos, Vector3 moonVel)>
            {
                { (new Vector3(-4, -14, 8), new Vector3(0)) },
                { (new Vector3(1, -8, 10), new Vector3(0)) },
                { (new Vector3(-15, 2, 1), new Vector3(0)) },
                { (new Vector3(-17, -17, 16), new Vector3(0)) }
            };

            List<(Vector3 moonPos, Vector3 moonSteps, bool cycleFound)> moonStart = new List<(Vector3 moonPos, Vector3 moonSteps, bool cycleFound)>
            {
                { (new Vector3(-4, -14, 8), new Vector3(0), false) },
                { (new Vector3(1, -8, 10), new Vector3(0), false) },
                { (new Vector3(-15, 2, 1), new Vector3(0), false) },
                { (new Vector3(-17, -17, 16), new Vector3(0), false) }
            };

            List<List<Vector3>> history = new List<List<Vector3>>
            {
                { new List<Vector3>() },
                { new List<Vector3>() },
                { new List<Vector3>() },
                { new List<Vector3>() }
            };

            history[0].Add(moons[0].moonPos);
            history[1].Add(moons[1].moonPos);
            history[2].Add(moons[2].moonPos);
            history[3].Add(moons[3].moonPos);

            Vector3[] newVels = new Vector3[moons.Count];
            int steps = 1;

            while (true)
            {
                steps++;

                for (int first = 0; first < moons.Count; first++)
                {
                    int x = 0;
                    int y = 0;
                    int z = 0;

                    for (int second = 0; second < moons.Count; second++)
                    {
                        if (first == second) continue;

                        x += (moons[first].moonPos.X > moons[second].moonPos.X) ? -1 : (moons[first].moonPos.X < moons[second].moonPos.X) ? 1 : 0;
                        y += (moons[first].moonPos.Y > moons[second].moonPos.Y) ? -1 : (moons[first].moonPos.Y < moons[second].moonPos.Y) ? 1 : 0;
                        z += (moons[first].moonPos.Z > moons[second].moonPos.Z) ? -1 : (moons[first].moonPos.Z < moons[second].moonPos.Z) ? 1 : 0;
                    }

                    Vector3 xyz = new Vector3(x, y, z);
                    newVels[first] = xyz;
                }

                for (int first = 0; first < moons.Count; first++)
                {
                    Vector3 newVel = moons[first].moonVel + newVels[first];
                    Vector3 newPos = moons[first].moonPos + newVel;

                    moons[first] = (newPos, newVel);

                    history[first].Add(newPos);

                    if (steps < 6 || moonStart[first].cycleFound) continue;

                    if (moonStart[first].moonSteps.X == 0 && 
                        history[first][0].X == history[first][steps-5].X &&
                        history[first][1].X == history[first][steps-4].X &&
                        history[first][2].X == history[first][steps-3].X &&
                        history[first][3].X == history[first][steps-2].X)
                    {
                        moonStart[first] = (moonStart[first].moonPos, new Vector3(steps - 5, moonStart[first].moonSteps.Y, moonStart[first].moonSteps.Z), moonStart[first].cycleFound);
                    }
                    if (moonStart[first].moonSteps.Y == 0 &&
                        history[first][0].Y == history[first][steps - 5].Y &&
                        history[first][1].Y == history[first][steps - 4].Y &&
                        history[first][2].Y == history[first][steps - 3].Y &&
                        history[first][3].Y == history[first][steps - 2].Y)
                    {
                        moonStart[first] = (moonStart[first].moonPos, new Vector3(moonStart[first].moonSteps.X, steps - 5, moonStart[first].moonSteps.Z), moonStart[first].cycleFound);
                    }
                    if (moonStart[first].moonSteps.Z == 0 &&
                        history[first][0].Z == history[first][steps - 5].Z &&
                        history[first][1].Z == history[first][steps - 4].Z &&
                        history[first][2].Z == history[first][steps - 3].Z &&
                        history[first][3].Z == history[first][steps - 2].Z)
                    {
                        moonStart[first] = (moonStart[first].moonPos, new Vector3(moonStart[first].moonSteps.X, moonStart[first].moonSteps.Y, steps - 5), moonStart[first].cycleFound);
                    }

                    if (moonStart[first].moonSteps.X != 0 && moonStart[first].moonSteps.Y != 0 && moonStart[first].moonSteps.Z != 0)
                    {
                        moonStart[first] = (moonStart[first].moonPos, moonStart[first].moonSteps, true);
                    }
                }

                if (moonStart.All(m => m.cycleFound)) break;
            }

            long xySteps = KgV((long)moonStart.Max(m => m.moonSteps.X), (long)moonStart.Max(m => m.moonSteps.Y));
            long xyzSteps = KgV(xySteps, (long) moonStart.Max(m => m.moonSteps.Z));

            Console.WriteLine(xyzSteps);
        }

        private static long KgV(long n1, long n2)
        {
            long max = Math.Max(n1, n2);
            long min = Math.Min(n1, n2);
            long kgV = max;
            
            while(kgV % min != 0)
            {
                kgV += max;
            }

            return kgV;
        }
    }
}
