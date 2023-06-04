using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using test_task.Engine;

namespace test_task
{
    internal class Program
    {
        /*
        public class TimeWatch
        {
            public void CheckTime()
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                Thread.Sleep(5000);
                stopwatch.Stop();

                TimeSpan ts = stopwatch.Elapsed;

                Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}",
                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            }
        }
        */

        static void Main(string[] args)
        {
            var torqueTable = new Dictionary<double, double>
            {
                { 20, 0 },
            };
            EngineSimulate engineSimulate = new EngineSimulate(10, torqueTable, 110, 0.01, 0.0001, 0.1);
            engineSimulate.Simulate(60, 0.01, 20);
        }
    }
}
