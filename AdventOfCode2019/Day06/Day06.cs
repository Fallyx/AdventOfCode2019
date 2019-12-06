using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode2019.Day06
{
    class Day06
    {
        const string inputPath = @"Day06/Input.txt";

        public static void Task1()
        {
            // key = orbiter, value = parent
            Dictionary<string, string> orbiterLink = new Dictionary<string, string>();
            int links = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    string[] obj = line.Split(')');

                    orbiterLink.Add(obj[1], obj[0]);
                }
            }

            foreach (var item in orbiterLink)
            {
                string currOrbiter = item.Key;
                string parent = item.Value;
                while (true)
                {
                    links++;
                    currOrbiter = parent;

                    if (currOrbiter == "COM") break;

                    parent = orbiterLink[currOrbiter];
                }
            }

            Console.WriteLine(links);
        }

        public static void Task2()
        {
            string line;
            // key = orbiter, value = parent
            Dictionary<string, string> orbiters = new Dictionary<string, string>();
            Dictionary<string, int> orbiterLinks = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] obj = line.Split(')');

                    orbiters.Add(obj[1], obj[0]);
                }
            }

            string currOrbiter = "SAN";
            string parent = orbiters[currOrbiter];
            int links = 0;

            while(parent != "COM")
            {
                if (orbiterLinks.ContainsKey(parent)) continue;
                
                orbiterLinks.Add(parent, links);
                currOrbiter = parent;
                parent = orbiters[currOrbiter];
                links++;
            }

            currOrbiter = "YOU";
            parent = orbiters[currOrbiter];
            links = 0;

            while (true)
            {
                if (orbiterLinks.ContainsKey(parent))
                {
                    Console.WriteLine(links + orbiterLinks[parent]);
                    break;
                }

                currOrbiter = parent;
                parent = orbiters[currOrbiter];
                links++;
            }
        }
    }
}
