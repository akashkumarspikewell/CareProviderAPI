using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Interfaces
{
    public interface IExperienceRepository
    {
        Task<IQueryable<Experience>> GetAllExperiencesAsync();
    }
}
