using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Interfaces
{
    public interface ICareProviderRepository
    {
        Task<IQueryable<CareProvider>> GetAllProvidersAsync();
        Task AddProviderAsync(CareProvider provider);
        Task<bool> DepartmentExists(int departmentid);
    }
}
