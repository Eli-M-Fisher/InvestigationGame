using System.Collections.Generic;
using InvestigationGame.Models.Sensors;

namespace InvestigationGame.Models.Agents
{
    public abstract class IranianAgent
    {
        protected List<string> _weaknesses;
        protected List<Sensor> _attachedSensors;

        public IranianAgent(List<string> weaknesses)
        {
            _weaknesses = weaknesses;
            _attachedSensors = new List<Sensor>();
        }

        public virtual void AttachSensor(Sensor sensor)
        {
            _attachedSensors.Add(sensor);
        }

        public int CountCorrectSensors()
        {
            int correct = 0;
            var remaining = new List<string>(_weaknesses); // Make a copy

            foreach (var sensor in _attachedSensors)
            {
                if (remaining.Contains(sensor.Name))
                {
                    correct++;
                    remaining.Remove(sensor.Name); // Prevent re-counting
                }
            }

            return correct;
        }

        public virtual string GetStatus()
        {
            return $"{CountCorrectSensors()}/{_weaknesses.Count}";
        }

        public virtual bool IsExposed()
        {
            return CountCorrectSensors() >= _weaknesses.Count;
        }

        public virtual void TakeTurn(int turnCount) { } // virtual hook
    }
}