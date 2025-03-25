using CareProviderAPI.Data.DTOs;
using CareProviderAPI.Data.Models;
using CareProviderAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CareProviderAPI.Services
{
    public class CareProviderService:ICareProviderService
    {
        private readonly ICareProviderRepository _repository;

        public CareProviderService(ICareProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProviderDto>> GetAllProvidersAsync()
        {
            IQueryable<CareProvider> query=await _repository.GetAllProvidersAsync();

            List<ProviderDto> result = await query
                .Include(p => p.Department)
                .Include(p => p.Achievements)
                .Where(p => p.IsActive)
                .Select(p => new ProviderDto
                {
                    ProviderID = p.ProviderId,
                    FullName = $"{p.FirstName} {p.LastName}",
                    Department = p.Department.DepartmentName,
                    Bio = p.Bio,
                    JoinDate = p.JoinDate,
                    IsActive = p.IsActive,
                    Achievements = p.Achievements.Any()
                        ? p.Achievements.Select(a => a.AchievementText).ToList()
                        : new List<string>(),
                    TotalExperienceYears = ((p.LeaveDate ?? DateTime.UtcNow) - p.JoinDate).Days / 365
                }).ToListAsync();

            return result;
        }

        public async Task AddProviderAsync(AddCareProviderDto providerDto)
        {
            if (string.IsNullOrWhiteSpace(providerDto.FirstName) || providerDto.FirstName.Length > 20)
                throw new ArgumentException("First name is required and must be under 20 characters.");

            if (string.IsNullOrWhiteSpace(providerDto.LastName) || providerDto.LastName.Length > 20)
                throw new ArgumentException("Last name is required and must be under 20 characters.");

            if (providerDto.DepartmentID <= 0)
                throw new ArgumentException("Invalid Department ID.");

            if (!string.IsNullOrEmpty(providerDto.Bio) && providerDto.Bio.Length > 500)
                throw new ArgumentException("Bio must be under 500 characters.");

            if (providerDto.LeaveDate.HasValue && providerDto.LeaveDate <= providerDto.JoinDate)
                throw new ArgumentException("Leave date cannot be before or equal to the join date.");

            var departmentExists = await _repository.DepartmentExists(providerDto.DepartmentID);
            if (!departmentExists)
                throw new ArgumentException("Department does not exist.");

            var provider = new CareProvider
            {
                FirstName = providerDto.FirstName,
                LastName = providerDto.LastName,
                Bio = providerDto.Bio,
                JoinDate = providerDto.JoinDate ?? DateTime.UtcNow,
                LeaveDate = providerDto.LeaveDate,
                IsActive = !providerDto.LeaveDate.HasValue,
                DepartmentID = providerDto.DepartmentID
            };

            await _repository.AddProviderAsync(provider);
        }

        public async Task<List<InactiveProviderDto>> GetInactiveProvidersAsync()
        {
            IQueryable<CareProvider> allProviders = await _repository.GetAllProvidersAsync();

            List<InactiveProviderDto> inactiveProviders = await allProviders
                .Include(p => p.Department)
                .Include(p => p.Achievements)
                .Where(p => !p.IsActive)
                .Select(p => new InactiveProviderDto
                {
                    ProviderID = p.ProviderId,
                    FullName = $"{p.FirstName} {p.LastName}",
                    Department = p.Department.DepartmentName,
                    Bio = p.Bio,
                    JoinDate = p.JoinDate,
                    LeaveDate=p.LeaveDate,
                    IsActive = p.IsActive,
                    Achievements = p.Achievements.Any()
                        ? p.Achievements.Select(a => a.AchievementText).ToList()
                        : new List<string>(),
                    TotalExperienceYears = ((p.LeaveDate ?? DateTime.UtcNow) - p.JoinDate).Days / 365
                }).ToListAsync();

            return inactiveProviders;
        }

        public async Task<List<ProviderDto>> GetProvidersByDepartmentAsync(int departmentId)
        {
            IQueryable<CareProvider> allProviders = await _repository.GetAllProvidersAsync();

            List<ProviderDto> providers = await allProviders
                .Include(p => p.Department)
                .Include(p => p.Achievements)
                .Where(p => p.IsActive)
                .Where(p => p.DepartmentID == departmentId)
                .Select(p => new ProviderDto
                {
                    ProviderID = p.ProviderId,
                    FullName = $"{p.FirstName} {p.LastName}",
                    Department = p.Department.DepartmentName,
                    Bio = p.Bio,
                    JoinDate = p.JoinDate,
                    IsActive = p.IsActive,
                    Achievements = p.Achievements.Any()
                        ? p.Achievements.Select(a => a.AchievementText).ToList()
                        : new List<string>(),
                    TotalExperienceYears = ((p.LeaveDate ?? DateTime.UtcNow) - p.JoinDate).Days / 365
                }).ToListAsync();

            return providers;
        }

        public async Task<List<ProviderDto>> GetByMinExperienceAsync(int minYears)
        {
            IQueryable<CareProvider> allProviders = await _repository.GetAllProvidersAsync();

            List<CareProvider> dbProviders = await allProviders
                .Include(p => p.Department)
                .Include(p => p.Achievements)
                .Where(p => p.IsActive)
                .ToListAsync();

            List<ProviderDto> filtered = dbProviders
                .Where(p => ((p.LeaveDate ?? DateTime.UtcNow) - p.JoinDate).TotalDays / 365 >= minYears)
                .Select(p => new ProviderDto
                {
                    ProviderID = p.ProviderId,
                    FullName = $"{p.FirstName} {p.LastName}",
                    Department = p.Department?.DepartmentName,
                    Bio = p.Bio,
                    JoinDate = p.JoinDate,
                    IsActive = p.IsActive,
                    Achievements = p.Achievements.Any()
                        ? p.Achievements.Select(a => a.AchievementText).ToList()
                        : new List<string>(),
                    TotalExperienceYears = (int)(((p.LeaveDate ?? DateTime.UtcNow) - p.JoinDate).TotalDays / 365)
                })
                .ToList();

            return filtered;
        }

    }
}
