using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IActivityRepository
        : IRepositoryBase<Activity, Guid>, IDataVersionProvider
    {

    }
}