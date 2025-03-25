using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IQueryable<Department>> GetAllDepartmentsAsync();
    }
}
