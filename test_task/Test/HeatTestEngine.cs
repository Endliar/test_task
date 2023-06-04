using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_task.Engine;

namespace test_task.Test
{
    public class HeatTestEngine
    {
        public static double RunTest(EngineSimulate engine, double simulationTime, double dt, double ambientTemperature)
        {
            engine.Simulate(simulationTime, dt, ambientTemperature);
            if (engine._CurrTemperature >= engine._T)
            {
                Console.WriteLine("Двигатель перегрелся, температура {0}", engine._CurrTemperature);
                return simulationTime;
            }
            Console.WriteLine("Тест завершён. Температура: {0}", engine._CurrTemperature);
            return -1;
        }
    }
}
