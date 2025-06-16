namespace InvestigationGame.Models.Sensors
{
    public abstract class Sensor
    {
        public string Name { get; }

        protected Sensor(string name)
        {
            Name = name;
        }

        // Called during activation phase (comparison to weakness)
        public abstract bool Activate(string weakness);

        // For logging or display
        public override string ToString()
        {
            return $"{GetType().Name} ({Name})";
        }
    }
}