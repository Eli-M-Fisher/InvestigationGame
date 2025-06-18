using System;
using InvestigationGame.Data;
using InvestigationGame.Models;

namespace InvestigationGame.Managers
{
    public static class GameMenu
    {
        public static void Start()
        {
            Console.WriteLine("=== Welcome to Investigation Game ===");
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            string connectionString = "server=localhost;user=root;password=;database=investigationgame;";
            MySqlPlayerStats stats = new MySqlPlayerStats(connectionString);
            Player player = stats.LoadPlayer(username);

            Console.WriteLine($"\nWelcome, {player.Name}! Resuming from level {player.CurrentAgentIndex}.");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Start Investigation");
                Console.WriteLine("2. Exit");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    InvestigationManager manager = new InvestigationManager(player, stats);
                    manager.StartInvestigation();
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("Exiting game. Goodbye!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
        }
    }
}