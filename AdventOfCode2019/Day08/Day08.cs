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
            char[,] lay = new char[imgHeight, imgWidth];

            for (int x = 0; x < imgHeight; x++)
            {
                for (int y = 0; y < imgWidth; y++)
                {
                    lay[x, y] = '2';
                }
            }

            foreach (var layer in layers)
            {
                for (int x = 0; x < layer.Length; x++)
                {
                    for (int y = 0; y < layer[x].Length; y++)
                    {
                        if (lay[x, y] == '2' && (layer[x][y] == '0' || layer[x][y] == '1'))
                        {
                            lay[x, y] = layer[x][y];
                        }
                    }
                }
            }

            for (int x = 0; x < imgHeight; x++)
            {
                for (int y = 0; y < imgWidth; y++)
                {
                    if (lay[x, y] == '0') Console.Write(' ');
                    else Console.Write(lay[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
