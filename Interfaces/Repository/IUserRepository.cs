using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserRepository : IRepositoryBase<User, string>
    {
        Task<IEnumerable<User>> SearchByUsernameAsync(string query, int page = 0, int size = 10);
        Task<User?> GetByUsernameAsync(string username);
        Task<string?> GetIdByUsernameAsync(string username);
    }
}