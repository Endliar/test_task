using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using test_task.Engine;
using test_task.Test;

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
            var torqueTableMV = new Dictionary<double, double>()
            {
                { 20, 0 },
                {75, 75 },
            };

            EngineSimulate engine = new EngineSimulate(10, torqueTableMV, 110, 0.01, 0.0001, 0.1);
            var maxPowerAndSpeed = MaxPowerTestEngine.RunTest(engine);
            Console.WriteLine("Максимальная мощность: {0} л.с., Скорость: {1} об/мин", maxPowerAndSpeed.Item1 * 1.36, maxPowerAndSpeed.Item2);
        }
    }
}
