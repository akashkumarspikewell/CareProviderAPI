using CareProviderAPI.Data.Context;
using CareProviderAPI.Data.Models;
using CareProviderAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareProviderAPI.Data.Repositories
{
    public class CareProviderRepository: ICareProviderRepository
    {
        private readonly AppDbContext _context;

        public CareProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable<CareProvider>> GetAllProvidersAsync()
        {
            return Task.FromResult(_context.CareProviders.AsQueryable());
        }

        public async Task AddProviderAsync(CareProvider provider)
        {
            await _context.CareProviders.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DepartmentExists(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentID == departmentId);
        }
    }
}
