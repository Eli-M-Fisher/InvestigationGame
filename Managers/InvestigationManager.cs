using System;
using System.Collections.Generic;
using InvestigationGame.Models.Agents;
using InvestigationGame.Models.Sensors;
using InvestigationGame.Models;
using InvestigationGame.Data;

namespace InvestigationGame.Managers
{
    public class InvestigationManager
    {
        private List<IranianAgent> _agentSequence;
        private int _currentAgentIndex;
        private IranianAgent _agent;
        private List<Sensor> _availableSensors;
        private int _turnCount;

        private Player _player;
        private MySqlPlayerStats _playerStats;

        public InvestigationManager(Player player, MySqlPlayerStats playerStats)
        {
            _player = player;
            _playerStats = playerStats;

            // Initialize available sensors
            _availableSensors = new List<Sensor>
            {
                new AudioSensor(),
                new PulseSensor(),
                new MagneticSensor(),
                new SignalSensor(),
                new LightSensor()
            };

            // Define agent sequence
            _agentSequence = new List<IranianAgent>
            {
                new FootSoldier(new List<string> { "Audio", "Pulse" }),
                new SquadLeader(new List<string> { "Pulse", "Pulse", "Audio", "Pulse" }),
                new SeniorCommander(new List<string> { "Pulse", "Audio", "Pulse", "Light", "Audio", "Magnetic" }),
                new OrganizationLeader(new List<string> { "Pulse", "Audio", "Light", "Pulse", "Magnetic", "Audio", "Pulse", "Signal" })
            };

            _currentAgentIndex = _player.CurrentAgentIndex;
            if (_currentAgentIndex >= _agentSequence.Count)
            {
                Console.WriteLine("You've already completed the game! Restarting from beginning.");
                _currentAgentIndex = 0;
            }

            _agent = _agentSequence[_currentAgentIndex];
            _turnCount = 0;
        }

        public void StartInvestigation()
        {
            Console.WriteLine("=== Iranian Agent Investigation ===");
            Console.WriteLine($"Welcome back, {_player.Name}!");
            Console.WriteLine($"Resuming at Level {_currentAgentIndex + 1} of {_agentSequence.Count}.\n");

            while (true)
            {
                _turnCount++;

                // Show current agent info at start of stage
                if (_turnCount == 1)
                {
                    Console.WriteLine($"\nNew Target: {_agent.GetType().Name}");
                    Console.WriteLine($"Level {_currentAgentIndex + 1} of {_agentSequence.Count}");
                    Console.WriteLine("Goal: Match all secret sensors to expose the agent.");
                }

                Console.WriteLine($"\n--- Turn {_turnCount} ---");
                Console.WriteLine("Available Sensors:");
                for (int i = 0; i < _availableSensors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_availableSensors[i]}");
                }
                Console.WriteLine("0. Exit and Save Progress");

                Console.Write($"Choose a sensor (1 - {_availableSensors.Count}, or 0 to exit): ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    SaveProgress();
                    Console.WriteLine("Progress saved. Goodbye!");
                    break;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _availableSensors.Count)
                {
                    Sensor selectedSensor = _availableSensors[choice - 1];

                    if (selectedSensor is PulseSensor pulse && pulse.IsBroken())
                    {
                        Console.WriteLine("This PulseSensor is broken. Choose a different one.");
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

                _agent.TakeTurn(_turnCount);

                if (_agent.IsExposed())
                {
                    Console.WriteLine("\nAgent exposed!");
                    _currentAgentIndex++;

                    // Save after every level
                    _player.CurrentAgentIndex = _currentAgentIndex;
                    _playerStats.UpdatePlayerProgress(_player);

                    if (_currentAgentIndex < _agentSequence.Count)
                    {
                        Console.WriteLine("\nAdvancing to next agent...");
                        _agent = _agentSequence[_currentAgentIndex];
                        _turnCount = 0;
                    }
                    else
                    {
                        Console.WriteLine("\nMission complete! All agents exposed.");
                        _player.CurrentAgentIndex = _agentSequence.Count;
                        _playerStats.UpdatePlayerProgress(_player);
                        break;
                    }
                }
            }
        }

        private void SaveProgress()
        {
            _player.CurrentAgentIndex = _currentAgentIndex;
            _playerStats.UpdatePlayerProgress(_player);
        }
    }
}