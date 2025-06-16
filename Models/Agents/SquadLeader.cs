using System;
using System.Collections.Generic;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Models.Agents
{
    public class SquadLeader : IranianAgent
    {
        public SquadLeader(List<string> weaknesses) : base(weaknesses) { }

        public override void TakeTurn(int turnCount)
        {
            if (turnCount % 3 == 0 && _attachedSensors.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(_attachedSensors.Count);
                var removed = _attachedSensors[index];
                _attachedSensors.RemoveAt(index);
                Console.WriteLine($"🛡 Counterattack! {removed.Name} sensor was removed.");
            }
        }
    }
}