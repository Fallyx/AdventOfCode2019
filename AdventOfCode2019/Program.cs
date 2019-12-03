using System;
using System.Diagnostics;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch swTot = new Stopwatch();
            Stopwatch swDay = new Stopwatch();

            swTot.Start();

            #region day 1
            swDay.Start();
            Day01.Day01.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 2
            swDay.Restart();
            Day02.Day02.Task1();
            Day02.Day02.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 02 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 3
            swDay.Restart();
            Day03.Day03.Task1();
            Console.WriteLine($"Day 03 elapsed time: {swDay.Elapsed}");
            swDay.Stop();
            #endregion

            swTot.Stop();

            Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}");
        }
    }
}
