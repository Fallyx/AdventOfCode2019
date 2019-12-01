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


            swTot.Stop();

            Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}");
        }
    }
}
