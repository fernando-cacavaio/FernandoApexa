namespace FernandoApexa.Application.Advisors
{
    public class AdvisorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SIN { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string HealthStatus { get; set; } = string.Empty;
    }
}