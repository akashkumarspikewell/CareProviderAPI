using CareProviderAPI.Data.Context;
using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Data.Repositories
{
    public class ExperienceRepository
    {
        private readonly AppDbContext _context;

        public ExperienceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable<Experience>> GetAllExperiencesAsync()
        {
            return Task.FromResult(_context.Experiences.AsQueryable());
        }
    }
}
