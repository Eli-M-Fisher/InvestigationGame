using InvestigationGame.Managers;

namespace InvestigationGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new InvestigationManager();
            manager.StartInvestigation();
        }
    }
}