namespace InvestigationGame.Models.Sensors
{
    public class PulseSensor : Sensor
    {
        private int _activationCount = 0;
        private const int MaxActivations = 3;

        public PulseSensor() : base("Pulse") {}

        public override bool Activate(string weakness)
        {
            if (_activationCount >= MaxActivations)
            {
                Console.WriteLine("PulseSensor is broken and cannot be used anymore.");
                return false;
            }

            _activationCount++;
            return Name == weakness;
        }

        public bool IsBroken()
        {
            return _activationCount >= MaxActivations;
        }

        public override string ToString()
        {
            string status = IsBroken() ? "Broken" : $"Uses left: {MaxActivations - _activationCount}";
            return $"{base.ToString()} → {status}";
        }
    }
}