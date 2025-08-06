using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Repository
{
    public class UserProfileRepository(
            ApplicationDbContext context
        ) : RepositoryBase<UserProfile, string>(context), IUserProfileRepository
    {

    }
}