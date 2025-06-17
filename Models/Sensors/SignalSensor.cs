using System;

namespace InvestigationGame.Models.Sensors
{
    public class SignalSensor : Sensor
    {
        public SignalSensor() : base("Signal") {}

        public override bool Activate(string weakness)
        {
            if (Name == weakness)
            {
                Console.WriteLine("SignalSensor reveals: Agent Rank → (e.g., SquadLeader)");
                return true;
            }

            return false;
        }
    }
}