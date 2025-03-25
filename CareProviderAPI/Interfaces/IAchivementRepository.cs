using CareProviderAPI.Data.Models;

namespace CareProviderAPI.Interfaces
{
    public interface IAchivementRepository
    {
        Task<IQueryable<Achievement>> GetAllAchivementsAsync();
    }
}
