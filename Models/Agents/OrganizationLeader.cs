using System;
using System.Collections.Generic;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Models.Agents
{
    public class OrganizationLeader : IranianAgent
    {
        private Dictionary<Sensor, int> _sensorShieldTurns = new(); // חיישנים מוגנים זמנית

        public OrganizationLeader(List<string> weaknesses) : base(weaknesses) {}

        public override void TakeTurn(int turnCount)
        {
            UpdateSensorShields();

            // Every 3 turns – try to remove one *unprotected* sensor
            if (turnCount % 3 == 0)
            {
                List<Sensor> unprotected = new List<Sensor>();
                foreach (var sensor in _attachedSensors)
                {
                    if (!_sensorShieldTurns.ContainsKey(sensor))
                        unprotected.Add(sensor);
                }

                if (unprotected.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(unprotected.Count);
                    var removed = unprotected[index];
                    _attachedSensors.Remove(removed);
                    Console.WriteLine($"Counterattack! {removed.Name} sensor was removed.");
                }
            }

            // Every 10 turns – full reset only if too few sensors attached
            if (turnCount % 10 == 0)
            {
                if (_attachedSensors.Count < 5)
                {
                    Console.WriteLine("Major counterattack! All attached sensors removed and weaknesses reshuffled!");
                    _attachedSensors.Clear();
                    _sensorShieldTurns.Clear();

                    // Build new weaknesses with at least 5 different types
                    List<string> possible = new List<string> { "Audio", "Pulse", "Signal", "Magnetic", "Light" };
                    Random rnd = new Random();
                    _weaknesses.Clear();

                    HashSet<string> selected = new HashSet<string>();
                    while (selected.Count < 5)
                        selected.Add(possible[rnd.Next(possible.Count)]);

                    while (selected.Count < 8)
                        selected.Add(possible[rnd.Next(possible.Count)]);

                    _weaknesses.AddRange(selected);
                }
                else
                {
                    Console.WriteLine("Major counterattack attempt blocked – too many active sensors!");
                }
            }
        }

        public override void AttachSensor(Sensor sensor)
        {
            base.AttachSensor(sensor);
            _sensorShieldTurns[sensor] = 2; // 2 turns of immunity
        }

        private void UpdateSensorShields()
        {
            var toRemove = new List<Sensor>();

            foreach (var kvp in _sensorShieldTurns)
            {
                _sensorShieldTurns[kvp.Key]--;
                if (_sensorShieldTurns[kvp.Key] <= 0)
                    toRemove.Add(kvp.Key);
            }

            foreach (var sensor in toRemove)
                _sensorShieldTurns.Remove(sensor);
        }
    }
}