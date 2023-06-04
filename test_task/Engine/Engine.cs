using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_task.Engine
{
    public class EngineSimulate
    {
        private double _L;
        private Dictionary<double, double> _torqueTableMV;
        private double _T;
        private double _heatingCoeff;
        private double _coolingCoeff;
        private double _CurrTemperature;
        private double _CurrSpeed;


        public EngineSimulate(double L, Dictionary<double, double> torqueTableMV,
            double T, double heatingCoeff, double coolingCoeff,
            double ambientTemperature) 
        { 
            _L = L;
            _torqueTableMV = torqueTableMV;
            _T = T;
            _heatingCoeff = heatingCoeff;
            _coolingCoeff = coolingCoeff;
            _CurrTemperature = ambientTemperature;
            _CurrSpeed = 0.0;
        }

        public void Simulate(double simulationTime, double dt, double ambientTemperature)
        {
            for (double time = 0; time < simulationTime; time+=dt)
            {
                double torque = GetTorque(_CurrSpeed);
                double acceleration = torque / _L;
                _CurrSpeed += acceleration * dt;
                double power = torque * _CurrSpeed / 1000;

                double heatingRate = torque * _heatingCoeff + Math.Pow(_CurrSpeed, 2) * _coolingCoeff;
                double coolingRate = _coolingCoeff * (ambientTemperature - _CurrTemperature);
                double temperatureChange = heatingRate - coolingRate * dt;
                _CurrTemperature = temperatureChange;
                if (_CurrTemperature >= _T) 
                {
                    break;
                }
            }
        }

        public double GetTorque(double speed)
        {
            double prevSpeed = 0;
            double prevTorque = 0;

            foreach(var pair in _torqueTableMV)
            {
                if (speed < pair.Key)
                {
                    double torque = Interpolate(prevTorque, pair.Value, prevSpeed, pair.Key, speed);
                    return torque;
                }
                prevSpeed = pair.Key;
                prevTorque = pair.Value;
            }

            double maxSpeed = _torqueTableMV.Keys.Last();
            double maxTorque = _torqueTableMV.Values.Last();
            double extrapolatedTorque = Extrapolate(prevTorque, maxTorque, prevSpeed, maxSpeed, speed);
            return extrapolatedTorque;
        }

        private double Interpolate(double y1, double y2, double x1, double x2, double x) 
        {
            return y1 + (y2 - y1) * (x1 - x2) / (x2 - x1);
        }

        private double Extrapolate(double y1, double y2, double x1, double x2, double x)
        {
            double k = (y2 - y1) / (x2 - x1);
            double b = y1 - k * x1;
            return k * x + b;
        }
    }
}
