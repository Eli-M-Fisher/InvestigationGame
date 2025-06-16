namespace InvestigationGame.Models
{
    public class Sensor
    {
        public string Name { get; }

        public Sensor(string name)
        {
            Name = name;
        }

        public bool Activate(string weakness)
        {
            return Name == weakness;
        }

        public override string ToString()
        {
            return $"Sensor: {Name}";
        }
    }
}