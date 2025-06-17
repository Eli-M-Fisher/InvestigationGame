using System;

namespace InvestigationGame.Models.Sensors
{
    public class LightSensor : Sensor
    {
        public LightSensor() : base("Light") {}

        public override bool Activate(string weakness)
        {
            if (Name == weakness)
            {
                Console.WriteLine("LightSensor reveals:");
                Console.WriteLine("• Agent Rank → (e.g., SquadLeader)");
                Console.WriteLine("• Number of Weaknesses → " + "Unknown"); // can be enhanced later
                return true;
            }

            return false;
        }
    }
}