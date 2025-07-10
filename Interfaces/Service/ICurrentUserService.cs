using System.Security.Claims;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface ICurrentUserService
    {
        Task<User?> GetCurrentUserAsync(ClaimsPrincipal user);
    }
}