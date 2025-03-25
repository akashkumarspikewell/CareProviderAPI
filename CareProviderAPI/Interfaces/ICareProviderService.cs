using CareProviderAPI.Data.DTOs;
using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Interfaces
{
    public interface ICareProviderService
    {
        Task<IEnumerable<ProviderDto>> GetAllProvidersAsync();
        Task AddProviderAsync(AddCareProviderDto providerDto);
        Task<List<InactiveProviderDto>> GetInactiveProvidersAsync();
        Task<List<ProviderDto>> GetProvidersByDepartmentAsync(int departmentId);
        Task<List<ProviderDto>> GetByMinExperienceAsync(int minYears);
    }
}
