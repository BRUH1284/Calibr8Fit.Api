using System.Security.Claims;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly UserManager<User> _userManager;

        public CurrentUserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userName = user?.Identity?.Name;
            if (string.IsNullOrEmpty(userName)) return null;

            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}