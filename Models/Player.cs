namespace InvestigationGame.Models
{
    public class Player
    {
        public int Id { get; set; }                 // מפתח ראשי (Primary Key)
        public string Name { get; set; }            // שם השחקן
        public int CurrentAgentIndex { get; set; }  // אינדקס הסוכן האחרון בו עצר

        public Player(string name, int currentAgentIndex = 0)
        {
            Name = name;
            CurrentAgentIndex = currentAgentIndex;
        }

        public override string ToString()
        {
            return $"Player: {Name}, CurrentAgentIndex: {CurrentAgentIndex}";
        }
    }
}