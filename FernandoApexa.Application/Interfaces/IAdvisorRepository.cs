using FernandoApexa.Domain;

namespace FernandoApexa.Application.Interfaces
{
    public interface IAdvisorRepository
    {
        Task<List<Advisor>> GetAllAdvisors();

        Task CreateAdvisor(Advisor advisor);

        Task DeleteAdvisor(int id);

        Task<Advisor> GetAdvisorById(int id);

        Task UpdateAdvisor();
    }
}