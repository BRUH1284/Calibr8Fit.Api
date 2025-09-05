using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IFoodRepository
        : IRepositoryBase<Food, int>, IDataVersionProvider
    {

    }
}
