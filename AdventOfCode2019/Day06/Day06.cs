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

                    if (currOrbiter == "COM")
                    {
                        break;
                    }

                    parent = orbiterLink[currOrbiter];
                }
            }

            Console.WriteLine(links);
        }

        public static void Task2()
        {
            string line;
            // key = orbiter, value = parent
            Dictionary<string, string> orbiterLink = new Dictionary<string, string>();
            Dictionary<string, int> orbitLinksYou = new Dictionary<string, int>();
            Dictionary<string, int> orbitLinksSan = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] obj = line.Split(')');

                    orbiterLink.Add(obj[1], obj[0]);
                }
            }

            string currOriterYou = "YOU";
            string parentYou = orbiterLink[currOriterYou];
            int linksYou = 0;

            string currOrbiterSan = "SAN";
            string parentSan = orbiterLink[currOrbiterSan];
            int linksSan = 0;

            while (true)
            {
                if (orbitLinksSan.ContainsKey(parentYou))
                {
                    Console.WriteLine(linksYou + orbitLinksSan[parentYou]); 
                    break;
                }
                else if (!orbitLinksYou.ContainsKey(parentYou))
                {
                    orbitLinksYou.Add(parentYou, linksYou);

                    currOriterYou = parentYou;
                    if (currOriterYou != "COM")
                    {
                        parentYou = orbiterLink[currOriterYou];
                        linksYou++;
                    }
                }

                if (orbitLinksYou.ContainsKey(parentSan))
                {
                    Console.WriteLine(linksSan + orbitLinksYou[parentSan]);
                    break;
                }
                else if (!orbitLinksSan.ContainsKey(parentSan))
                {
                    orbitLinksSan.Add(parentSan, linksSan);

                    currOrbiterSan = parentSan;
                    if (currOrbiterSan != "COM")
                    {
                        parentSan = orbiterLink[currOrbiterSan];
                        linksSan++;
                    }
                }
            }
        }
    }
}
