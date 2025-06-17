using System;
using System.Collections.Generic;
using InvestigationGame.Models.Agents;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Managers
{
    public class InvestigationManager
    {
        private List<IranianAgent> _agentSequence;
        private int _currentAgentIndex;
        private IranianAgent _agent;
        private List<Sensor> _availableSensors;
        private int _turnCount;

        public InvestigationManager()
        {
            // Initialize available sensors (basic + advanced)
            _availableSensors = new List<Sensor>
            {
                new AudioSensor(),
                new PulseSensor(),
                new MagneticSensor(),
                new SignalSensor(),
                new LightSensor()
            };

            // Initialize agent progression
            _agentSequence = new List<IranianAgent>
            {
                new FootSoldier(new List<string> { "Audio", "Pulse" }),
                new SquadLeader(new List<string> { "Pulse", "Pulse", "Audio", "Pulse" })
            };

            _currentAgentIndex = 0;
            _agent = _agentSequence[_currentAgentIndex];
            _turnCount = 0;
        }

        public void StartInvestigation()
        {
            Console.WriteLine("=== Iranian Agent Investigation ===");

            while (true)
            {
                _turnCount++;

                // Show current level and target info at start of each agent
                if (_turnCount == 1)
                {
                    Console.WriteLine($"\nNew Target: {_agent.GetType().Name}");
                    Console.WriteLine($"Level {_currentAgentIndex + 1} of {_agentSequence.Count}");
                    Console.WriteLine($"Goal: Reveal this agent by matching all secret sensors.");
                }

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

                // Let the agent act (e.g., counterattack)
                _agent.TakeTurn(_turnCount);

                // Check if agent was exposed
                if (_agent.IsExposed())
                {
                    Console.WriteLine("\nAgent exposed!");
                    _currentAgentIndex++;

                    if (_currentAgentIndex < _agentSequence.Count)
                    {
                        Console.WriteLine("\nAdvancing to next agent...\n");
                        _agent = _agentSequence[_currentAgentIndex];
                        _turnCount = 0;
                    }
                    else
                    {
                        Console.WriteLine("\nMission complete! All agents exposed.");
                        break;
                    }
                }
            }
        }
    }
}