namespace FernandoApexa.Domain
{
    public class Advisor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SIN { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string HealthStatus { get; set; } = string.Empty;
    }
}