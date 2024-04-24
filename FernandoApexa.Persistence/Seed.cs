using FernandoApexa.Application.Advisors;
using FernandoApexa.Domain;

namespace FernandoApexa.Persistence
{
    public class Seed
    {
        public static async Task SeedData(FernandoApexaDbContext context)
        {
            if (!context.Advisors.Any())
            {
                var advisors = new List<Advisor>
                {
                    new Advisor
                    {
                       Address = "Fernando Addres 1",
                       HealthStatus = new HealthStatus().GetRandomName(),
                       Name = "Fernando 1",
                       Phone = "11111111",
                       SIN = "111111111"
                    },
                     new Advisor
                    {
                       Address = "Fernando Addres 2",
                       HealthStatus = new HealthStatus().GetRandomName(),
                       Name = "Fernando 2",
                       Phone = "22222222",
                       SIN = "222222222"
                    },
                      new Advisor
                    {
                       Address = "Fernando Addres 3",
                       HealthStatus = new HealthStatus().GetRandomName(),
                       Name = "Fernando 3",
                       Phone = "33333333",
                       SIN = "333333333"
                    },
                };

                await context.Advisors.AddRangeAsync(advisors);
                await context.SaveChangesAsync();
            }
        }
    }
}