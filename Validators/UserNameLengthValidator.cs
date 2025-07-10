using Microsoft.AspNetCore.Identity;

namespace Calibr8Fit.Api.Validators
{
    public class UserNameLengthValidator<TUser> : IUserValidator<TUser> where TUser : IdentityUser
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public UserNameLengthValidator(int minLength = 5, int maxLength = 32)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            // Validate the username length
            if (string.IsNullOrWhiteSpace(user.UserName) || user.UserName.Length < _minLength)
            {
                if (user.UserName!.Length < _minLength || user.UserName.Length > _maxLength)
                    return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {
                        Code = "UsernameLength",
                        Description = $"Username must be between {_minLength} and {_maxLength} characters."
                    }));
            }
            // Username is valid
            return Task.FromResult(IdentityResult.Success);
        }
    }
}