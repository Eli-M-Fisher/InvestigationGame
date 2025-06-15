using System;
using System.Collections.Generic;
using InvestigationGame.Models;

namespace InvestigationGame.Managers
{
    public class InvestigationManager
    {
        private IranianAgent _agent;
        private List<Sensor> _availableSensors;

        public InvestigationManager()
        {
            // Step 1: Initialize available sensors
            _availableSensors = new List<Sensor>
            {
                new Sensor("Thermal"),
                new Sensor("Audio")
            };

            // Step 2: Initialize agent with secret weaknesses (example: Thermal + Thermal)
            var weaknesses = new List<string> { "Thermal", "Thermal" };
            _agent = new IranianAgent(weaknesses);
        }

        public void StartInvestigation()
        {
            Console.WriteLine("=== Iranian Agent Investigation ===");
            while (!_agent.IsExposed())
            {
                Console.WriteLine("\nAvailable Sensors:");
                for (int i = 0; i < _availableSensors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_availableSensors[i]}");
                }

                Console.Write("Choose a sensor to attach (1 or 2): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= _availableSensors.Count)
                {
                    Sensor selectedSensor = _availableSensors[choice - 1];
                    _agent.AttachSensor(selectedSensor);
                    Console.WriteLine($"Sensor attached: {selectedSensor.Name}");

                    string status = _agent.GetStatus();
                    Console.WriteLine($"Match status: {status}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            Console.WriteLine("\nAgent exposed!");
        }
    }
}