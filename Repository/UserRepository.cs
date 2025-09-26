using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class UserRepository(
        ApplicationDbContext context
    ) : RepositoryBase<User, string>(context), IUserRepository
    {
        public async Task<IEnumerable<User>> SearchByUsernameAsync(string query)
        {
            query = query.Trim().ToLowerInvariant();

            return await _dbSet
                .Where(u => u.UserName != null && u.UserName.Contains(query))
                .Take(10)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _dbSet.FirstOrDefaultAsync(u => u.UserName == username);

        public async Task<string?> GetIdByUsernameAsync(string username) =>
            await _dbSet
                .Where(u => u.UserName == username)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
    }
}