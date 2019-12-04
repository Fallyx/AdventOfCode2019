using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Day04
{
    class Day04
    {
        static int min = 134792;
        static int max = 675810;

        public static void Task1and2()
        {
            int numHits = 0;
            int numHits2 = 0;

            for(int i = min; i <= max; i++)
            {
                bool adj = false;
                bool bad = false;

                int[] numbers = new int[10];

                string num = i.ToString();

                for(int x = 0; x < num.Length - 1; x++)
                {
                    int idx;

                    if (x == 0)
                    {
                        idx = int.Parse(num[5].ToString());
                        numbers[idx] += 1;
                    }

                    if(num[x] > num[x+1])
                    {
                        bad = true;
                        break;
                    }
                    else if(num[x] == num[x+1])
                    {
                        adj = true;
                    }

                    idx = int.Parse(num[x].ToString());
                    numbers[idx] += 1;
                }

                if (!bad && adj)
                {
                    numHits++;

                    foreach(int n in numbers) 
                    {
                        if (n == 2)
                        {
                            numHits2++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(numHits);
            Console.WriteLine(numHits2);
        }
    }
}
