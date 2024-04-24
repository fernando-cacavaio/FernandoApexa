using FernandoApexa.Application.Interfaces;

namespace FernandoApexa.Application.Advisors
{
    public class HealthStatus : IHealthStatus
    {
        public string Name { get; set; } = string.Empty;
        public float Probability { get; set; }

        public string GetRandomName()
        {
            var listOfNames = new HealthStatus[]
            {
            new HealthStatus { Name = "Green", Probability = 60f },
            new HealthStatus { Name = "Yellow", Probability = 20f },
            new HealthStatus { Name = "Red", Probability = 20f }
            };

            float totalProbability = listOfNames.Sum(nameInfo => nameInfo.Probability);
            double randomValue = new Random().NextDouble() * totalProbability;

            foreach (var nameInfo in listOfNames)
            {
                randomValue -= nameInfo.Probability;
                if (randomValue <= 0f)
                {
                    return nameInfo.Name;
                }
            }

            return null;
        }
    }
}