using System;
using System.Collections.Generic;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Models.Agents
{
    public class SeniorCommander : IranianAgent
    {
        public SeniorCommander(List<string> weaknesses) : base(weaknesses) {}

        public override void TakeTurn(int turnCount)
        {
            if (turnCount % 3 == 0 && _attachedSensors.Count > 0)
            {
                int sensorsToRemove = Math.Min(2, _attachedSensors.Count);
                Random random = new Random();

                for (int i = 0; i < sensorsToRemove; i++)
                {
                    int index = random.Next(_attachedSensors.Count);
                    var removed = _attachedSensors[index];
                    _attachedSensors.RemoveAt(index);
                    Console.WriteLine($"Counterattack! {removed.Name} sensor was removed.");
                }
            }
        }
    }
}