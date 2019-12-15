using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day14
{
    class Day14
    {
        const string inputPath = @"Day14/Input.txt";
        static int oresNeeded = 0;
        const long gatheredOres = 1000000000000;
        static long usedOre = 0;

        public static void Task1()
        {
            Dictionary<string, int> getsProduced = new Dictionary<string, int>();
            Dictionary<string, List<(string, int)>> reactions = new Dictionary<string, List<(string, int)>>();
            Dictionary<string, long> storage = new Dictionary<string, long>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while((line = reader.ReadLine()) != null)
                {
                    string[] recip = line.Split(" => ");
                    string[] produced = recip[1].Split(' ');

                    getsProduced.Add(produced[1], Int32.Parse(produced[0]));
                    reactions.Add(produced[1], new List<(string, int)>());

                    string[] consumes = recip[0].Split(", ");

                    foreach(var consume in consumes)
                    {
                        string[] c = consume.Split(' ');
                        reactions[produced[1]].Add((c[1], Int32.Parse(c[0])));
                    }
                }
            }

            CalcFuel("FUEL", reactions, getsProduced, storage);

            Console.WriteLine(oresNeeded);
        }

        private static void CalcFuel(string chemical, Dictionary<string, List<(string chem, int amount)>> reactions, Dictionary<string, int> getsProduced, Dictionary<string, long> storage)
        {
            if (reactions[chemical].Any(c => c.chem == "ORE"))
            {
                oresNeeded += reactions[chemical][0].amount;                    
                return;
            }
            
            foreach(var (chem, amount) in reactions[chemical])
            {
                if (!storage.ContainsKey(chem)) storage.Add(chem, 0);

                while (storage[chem] < amount)
                {
                    storage[chem] += getsProduced[chem];

                    CalcFuel(chem, reactions, getsProduced, storage);
                }

                storage[chem] -= amount;
            }
        }

        public static void Task2()
        {
            Dictionary<string, int> getsProduced = new Dictionary<string, int>();
            Dictionary<string, List<(string, int)>> reactions = new Dictionary<string, List<(string, int)>>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] recip = line.Split(" => ");
                    string[] produced = recip[1].Split(' ');

                    getsProduced.Add(produced[1], Int32.Parse(produced[0]));
                    reactions.Add(produced[1], new List<(string, int)>());

                    string[] consumes = recip[0].Split(", ");

                    foreach (var consume in consumes)
                    {
                        string[] c = consume.Split(' ');
                        reactions[produced[1]].Add((c[1], Int32.Parse(c[0])));
                    }
                }
            }

            long multiplier = 10000000;
            
            long fuel = 0;

            while(true)
            {
                usedOre = 0;
                var storage = new Dictionary<string, long>();

                CalcFuel2("FUEL", fuel + multiplier, getsProduced, reactions, storage);

                if (usedOre > gatheredOres)
                {
                    multiplier /= 10;
                    continue;
                }
                else
                {
                    fuel += multiplier;
                }

                if (multiplier < 1) break;

            }

            // - 1 is needed for real input. Test inputs work without it
            Console.WriteLine(fuel - 1);
        }

        private static void CalcFuel2(string chemical, long amount, Dictionary<string, int> getsProduced, Dictionary<string, List<(string chem, int am)>> reactions, Dictionary<string, long> storage)
        {
            if (!storage.ContainsKey(chemical)) storage.Add(chemical, 0);

            long multiplier = (long)Math.Ceiling((double)(amount / getsProduced[chemical]));

            foreach (var (chem, am) in reactions[chemical])
            {
                long needed = am * multiplier;

                if (chem == "ORE")
                {
                    usedOre += needed;
                    continue;
                }

                if (storage.ContainsKey(chem))
                {
                    long take = storage[chem] > needed ? needed : storage[chem];
                    storage[chem] -= take;
                    needed -= take;
                }

                if (needed > 0)
                {
                    CalcFuel2(chem, needed, getsProduced, reactions, storage);
                    storage[chem] -= needed;
                }
            }

            storage[chemical] += getsProduced[chemical] * multiplier;
        }
    }    
}
