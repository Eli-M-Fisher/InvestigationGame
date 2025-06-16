namespace InvestigationGame.Models.Sensors
{
    public class AudioSensor : Sensor
    {
        public AudioSensor() : base("Audio") {}

        public override bool Activate(string weakness)
        {
            return Name == weakness;
        }
    }
}