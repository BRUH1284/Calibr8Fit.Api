using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers.Abstract
{
    public abstract class AppControllerBase(
        ICurrentUserService currentUserService
    ) : ControllerBase
    {
        protected readonly ICurrentUserService _currentUserService = currentUserService;

        // Helper method to execute an action with the current user
        protected async Task<IActionResult> WithUser(Func<User, Task<IActionResult>> action)
        {
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");
            return await action(user);
        }
        protected async Task<IActionResult> WithUserId(Func<string, Task<IActionResult>> action)
        {
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");
            return await action(user.Id);
        }
    }
}