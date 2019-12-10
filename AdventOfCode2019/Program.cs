using System;
using System.Diagnostics;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            FullRun();
            // SingleRun();
        }

        private static void FullRun()
        {
            Stopwatch swTot = new Stopwatch();
            Stopwatch swDay = new Stopwatch();

            swTot.Start();

            #region day 1
            swDay.Start();
            Day01.Day01.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 2
            swDay.Restart();
            Day02.Day02.Task1();
            Day02.Day02.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 02 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 3
            swDay.Restart();
            Day03.Day03.Task1and2();
            Console.WriteLine($"Day 03 elapsed time: {swDay.Elapsed}\n");
            swDay.Stop();
            #endregion

            #region day 4
            swDay.Restart();
            Day04.Day04.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 04 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 5
            swDay.Restart();
            Day05.Day05.Task1();
            Day05.Day05.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 05 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 6
            swDay.Restart();
            Day06.Day06.Task1();
            Day06.Day06.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 06 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 7
            swDay.Restart();
            Day07.Day07.Task1();
            Day07.Day07.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 07 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 8
            swDay.Restart();
            Day08.Day08.Task1();
            swDay.Stop();
            Console.WriteLine($"Day 08 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 9
            swDay.Restart();
            Day09.Day09.Task1();
            Day09.Day09.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 09 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 10
            swDay.Restart();
            Day10.Day10.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 10 elapsed time: {swDay.Elapsed}\n");
            #endregion

            swTot.Stop();

            Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}");
        }

        private static void SingleRun()
        {
            Day10.Day10.Task1and2();
        }
    }
}
