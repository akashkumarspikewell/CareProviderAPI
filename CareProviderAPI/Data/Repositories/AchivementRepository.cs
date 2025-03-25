using CareProviderAPI.Data.Context;
using CareProviderAPI.Data.Models;
using CareProviderAPI.Interfaces;

namespace CareProviderAPI.Data.Repositories
{
    public class AchivementRepository : IAchivementRepository
    {
        private readonly AppDbContext _context;

        public AchivementRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable<Achievement>> GetAllAchivementsAsync()
        {
            return Task.FromResult(_context.Achievements.AsQueryable());
        }
    }
}
