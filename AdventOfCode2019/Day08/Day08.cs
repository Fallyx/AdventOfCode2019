using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day08
{
    class Day08
    {
        const string inputPath = @"Day08/Input.txt";
        const int imgWidth = 25;
        const int imgHeight = 6;

        public static void Task1()
        {
            string line;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                line = reader.ReadLine();
            }

            List<string[]> layers = new List<string[]>();
            int pos = 0;

            while (pos < line.Length)
            {
                string[] layer = new string[imgHeight];

                for (int i = 0; i < imgHeight; i++)
                {
                    string width = line.Substring(pos, imgWidth);
                    layer[i] = width;
                    pos += imgWidth;
                }
                layers.Add(layer);
            }

            Console.WriteLine(CalcCorrupted(layers));

            PrintCode(layers);
        }

        private static int CalcCorrupted(List<string[]> layers)
        {
            int minAmountOf0 = int.MaxValue;
            int corruptNumber = 0;

            foreach (var layer in layers)
            {
                int zeros = 0;
                int ones = 0;
                int twos = 0;
                foreach (var l in layer)
                {
                    zeros += l.Count(n => n == '0');
                    ones += l.Count(n => n == '1');
                    twos += l.Count(n => n == '2');
                }

                if (zeros < minAmountOf0)
                {
                    corruptNumber = ones * twos;
                    minAmountOf0 = zeros;
                }
            }

            return corruptNumber;
        }

        private static void PrintCode(List<string[]> layers)
        {
            for (int x = 0; x < layers[0].Length; x++)
            {
                for (int y = 0; y < layers[0][0].Length; y++)
                {
                    foreach (var layer in layers)
                    {
                        if (layer[x][y] == '2') continue;

                        if (layer[x][y] == '0')
                        {
                            Console.Write(' ');
                            break;
                        }
                        else
                        {
                            Console.Write(layer[x][y]);
                            break;
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
