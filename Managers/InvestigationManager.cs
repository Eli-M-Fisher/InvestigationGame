using System;
using System.Collections.Generic;
using InvestigationGame.Models.Agents;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Managers
{
    public class InvestigationManager
    {
        private IranianAgent _agent;
        private List<Sensor> _availableSensors;
        private int _turnCount;

        public InvestigationManager()
        {
            _availableSensors = new List<Sensor>
            {
                new AudioSensor(),
                new PulseSensor()
            };

            // Example: use SquadLeader with 4 weaknesses
            var weaknesses = new List<string> { "Pulse", "Pulse", "Audio", "Pulse" };
            _agent = new SquadLeader(weaknesses);
            _turnCount = 0;
        }

        public void StartInvestigation()
        {
            Console.WriteLine("=== Iranian Agent Investigation ===");

            while (!_agent.IsExposed())
            {
                _turnCount++;

                Console.WriteLine($"\n--- Turn {_turnCount} ---");
                Console.WriteLine("Available Sensors:");
                for (int i = 0; i < _availableSensors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_availableSensors[i]}");
                }

                Console.Write($"Choose a sensor to attach (1 - {_availableSensors.Count}): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _availableSensors.Count)
                {
                    Sensor selectedSensor = _availableSensors[choice - 1];

                    if (selectedSensor is PulseSensor pulse && pulse.IsBroken())
                    {
                        Console.WriteLine("This PulseSensor is already broken. Choose a different one.");
                        continue;
                    }

                    _agent.AttachSensor(selectedSensor);
                    Console.WriteLine($"Sensor attached: {selectedSensor.Name}");
                    Console.WriteLine($"Match status: {_agent.GetStatus()}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                // Run agent's turn logic (e.g., counterattack)
                _agent.TakeTurn(_turnCount);
            }

            Console.WriteLine("\nAgent exposed!");
        }
    }
}