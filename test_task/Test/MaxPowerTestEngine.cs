using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_task.Engine;

namespace test_task.Test
{
    public class MaxPowerTestEngine
    {
        public static Tuple<double, double> RunTest(EngineSimulate engine)
        {
            double maxPower = engine._torqueTableMV.First().Value;
            double maxSpeed = engine._torqueTableMV.Last().Value;

            for (double speed = 0; ; speed += 100)
            {
                double torque = engine.GetTorque(speed);
                if (torque == 0)
                {
                    Console.WriteLine("Двигатель больше не раскручивается");
                    break;
                }
                double power = torque * speed / 1000;
                if (power > maxPower)
                {
                    maxPower = power;
                    maxSpeed = speed;
                }
            }
            Console.WriteLine("Тест завершен. Максимальная мощность: {0}, Скорость: {1}", maxPower, maxSpeed);
            return Tuple.Create(maxPower, maxSpeed);
        }
    }
}
