using FernandoApexa.Application.Interfaces;
using FernandoApexa.Domain;
using Microsoft.EntityFrameworkCore;

namespace FernandoApexa.Persistence.Repositories
{
    public class AdvisorRepository : IAdvisorRepository
    {
        private readonly FernandoApexaDbContext _context;

        public AdvisorRepository(FernandoApexaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Advisor>> GetAllAdvisors()
        {
            return await _context.Advisors.ToListAsync();
        }

        public async Task CreateAdvisor(Advisor advisor)
        {
            await _context.Advisors.AddAsync(advisor);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdvisor(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor != null) _context.Remove(advisor);

            await _context.SaveChangesAsync();
        }

        public async Task<Advisor> GetAdvisorById(int id)
        {
            return await _context.Advisors.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAdvisor()
        {
            await _context.SaveChangesAsync();
        }
    }
}