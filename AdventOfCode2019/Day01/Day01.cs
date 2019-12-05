using System;
using System.IO;

namespace AdventOfCode2019.Day01
{
    class Day01
    {
        const string inputPath = @"Day01/Input.txt";

        public static void Task1and2()
        {
            int fuelReqTask1 = 0;
            int fuelReqTask2 = 0;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int mass = int.Parse(line);
                    int fuel = (int)(mass / 3d) - 2;
                    fuelReqTask1 += fuel;

                    while (fuel > 0)
                    {
                        fuelReqTask2 += fuel;
                        mass = fuel;
                        fuel = (int)(mass / 3d) - 2;
                    }
                }
            }

            Console.WriteLine(fuelReqTask1);
            Console.WriteLine(fuelReqTask2);
        }
    }
}
