using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Drawing;

namespace AdventOfCode2019.Day10
{
    class Day10
    {
        const string inputPath = @"Day10/Input.txt";

        public static void Task1and2()
        {
            Dictionary<Point, HashSet<double>> asteroids = new Dictionary<Point, HashSet<double>>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int y = 0;
                while((line = reader.ReadLine()) != null)
                {
                    for(int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '#') asteroids.Add(new Point(i, y), new HashSet<double>());
                    }
                    y++;
                }
            }

            foreach(var asteroid in asteroids)
            {
                foreach(var asteroid2 in asteroids)
                {
                    if (asteroid.Key == asteroid2.Key) continue;

                    double rad = Math.Atan2(asteroid.Key.Y - asteroid2.Key.Y, asteroid.Key.X - asteroid2.Key.X);

                    if (!asteroid.Value.Contains(rad)) asteroid.Value.Add(rad);
                }
            }

            Console.WriteLine(asteroids.Max(a => a.Value.Count));

            Point chosenAsteroid = asteroids.OrderByDescending(a => a.Value.Count).First().Key;
            SortedDictionary<double, List<Point>> asteroidDestroy = new SortedDictionary<double, List<Point>>();

            foreach(var asteroid in asteroids)
            {
                if (asteroid.Key == chosenAsteroid) continue;

                double rad = Math.Atan2(chosenAsteroid.Y - asteroid.Key.Y, chosenAsteroid.X - asteroid.Key.X);
                double deg = rad * (180 / Math.PI);
                deg = ((deg - 90) + 360) % 360;

                if (!asteroidDestroy.ContainsKey(deg)) asteroidDestroy.Add(deg, new List<Point>());

                asteroidDestroy[deg].Add(asteroid.Key);
            }

            int countDestroyed = 0;
            while(true)
            {
                foreach(var aD in asteroidDestroy)
                { 
                    var candidates = aD.Value;

                    Point candidate = candidates.Aggregate((l, r) => Math.Pow(chosenAsteroid.Y - l.Y, 2) + Math.Pow(chosenAsteroid.X - l.X, 2) < Math.Pow(chosenAsteroid.Y - r.Y, 2) + Math.Pow(chosenAsteroid.X - r.X, 2) ? l : r);              

                    aD.Value.Remove(candidate);
                    countDestroyed++;

                    if (countDestroyed == 200)
                    {
                        Console.WriteLine(candidate.X * 100 + candidate.Y);
                        return;
                    }
                }
            }
        }
    }
}
