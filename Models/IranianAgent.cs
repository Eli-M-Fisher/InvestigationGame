using System.Collections.Generic;

namespace InvestigationGame.Models
{
    public class IranianAgent
    {
        private readonly List<string> _weaknesses;
        private readonly List<Sensor> _attachedSensors;

        public IranianAgent(List<string> weaknesses)
        {
            _weaknesses = weaknesses;
            _attachedSensors = new List<Sensor>();
        }

        public void AttachSensor(Sensor sensor)
        {
            _attachedSensors.Add(sensor);
        }

        public int CountCorrectSensors()
        {
            int correctCount = 0;
            foreach (var sensor in _attachedSensors)
            {
                if (_weaknesses.Contains(sensor.Name))
                {
                    correctCount++;
                }
            }
            return correctCount;
        }

        public bool IsExposed()
        {
            return CountCorrectSensors() >= _weaknesses.Count;
        }

        public string GetStatus()
        {
            return $"{CountCorrectSensors()}/{_weaknesses.Count}";
        }
    }
}