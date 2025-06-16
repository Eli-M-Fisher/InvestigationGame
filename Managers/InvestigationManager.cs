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

        public InvestigationManager()
        {
            // Step 1: Initialize available sensors
            _availableSensors = new List<Sensor>
            {
                new AudioSensor(),
                new PulseSensor()
            };

            // Step 2: Initialize a basic agent with known weaknesses
            var weaknesses = new List<string> { "Pulse", "Pulse" };
            _agent = new FootSoldier(weaknesses);
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

                Console.Write($"Choose a sensor to attach (1 - {_availableSensors.Count}): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _availableSensors.Count)
                {
                    Sensor selectedSensor = _availableSensors[choice - 1];

                    // Prevent attaching broken sensors (e.g. PulseSensor)
                    if (selectedSensor is PulseSensor pulse && pulse.IsBroken())
                    {
                        Console.WriteLine("⚠️ This PulseSensor is already broken. Choose a different one.");
                        continue;
                    }

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

            Console.WriteLine("\n✅ Agent exposed!");
        }
    }
}