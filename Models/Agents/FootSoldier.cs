using System.Collections.Generic;

namespace InvestigationGame.Models.Agents
{
    public class FootSoldier : IranianAgent
    {
        public FootSoldier(List<string> weaknesses) : base(weaknesses) { }

        // No counterattack
    }
}