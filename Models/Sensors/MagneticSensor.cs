using System;

namespace InvestigationGame.Models.Sensors
{
    public class MagneticSensor : Sensor
    {
        private int _usesLeft = 2;

        public MagneticSensor() : base("Magnetic") {}

        public override bool Activate(string weakness)
        {
            if (_usesLeft > 0 && Name == weakness)
            {
                _usesLeft--;
                Console.WriteLine("Magnetic field disrupts counterattack this turn!");
                return true;
            }

            return Name == weakness;
        }

        public bool CanBlockCounter()
        {
            return _usesLeft > 0;
        }

        public override string ToString()
        {
            return $"{base.ToString()} → Counter blocks left: {_usesLeft}";
        }
    }
}