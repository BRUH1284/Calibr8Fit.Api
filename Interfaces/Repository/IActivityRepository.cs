using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IActivityRepository : IDataVersionProvider
    {
        Task<Activity?> AddAsync(Activity activity);
        Task<Activity?> GetByIdAsync(Guid id);
        Task<List<Activity>> GetAllAsync();
        Task<Activity?> UpdateAsync(Guid id, UpdateActivityRequestDto activityDto);
        Task<Activity?> DeleteAsync(Guid id);
    }
}