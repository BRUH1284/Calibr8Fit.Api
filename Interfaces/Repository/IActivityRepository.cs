using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IActivityRepository : IDataVersionProvider
    {
        Task<Activity?> AddAsync(Activity activity);
        Task<Activity?> GetByCodeAsync(int code);
        Task<List<Activity>> GetAllAsync();
        Task<Activity?> UpdateAsync(int code, UpdateActivityRequestDto activityDto);
        Task<Activity?> DeleteAsync(int code);
    }
}