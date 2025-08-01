using Calibr8Fit.Api.DataTransferObjects.Authentication;
using Calibr8Fit.Api.DataTransferObjects.Token;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Services
{
    public class AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepo,
        IUserProfileRepository userProfileRepo) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepo = refreshTokenRepo;
        private readonly IUserProfileRepository _userProfileRepo = userProfileRepo;

        public async Task<IdentityResult<TokenDto>> RegisterUserAsync(RegisterDto registerDto)
        {
            // Crete new user
            var user = new User
            {
                UserName = registerDto.UserName
            };

            // Check if user already exists
            var existingUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingUser != null)
                return IdentityResult<TokenDto>.Failure([
                    new IdentityError {
                        Code = "DuplicateUserName",
                        Description = "User with this username already exists."
                    }
                ]);

            // Save new user
            try
            {
                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (!createdUser.Succeeded)
                    return IdentityResult<TokenDto>.Failure(createdUser.Errors);
            }
            catch (Exception ex)
            {
                var identityError = new IdentityError
                {
                    Code = "Exception",
                    Description = ex.InnerException?.Message ?? ex.Message
                };
                return IdentityResult<TokenDto>.Failure([identityError]);
            }

            // Create user profile
            var userProfile = new UserProfile
            {
                UserId = user.Id
            };

            // Save user profile
            await _userProfileRepo.AddAsync(userProfile);

            // Add role
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
                return IdentityResult<TokenDto>.Failure(roleResult.Errors);

            // Retrieve roles
            var roles = await _userManager.GetRolesAsync(user);

            return IdentityResult<TokenDto>.Success(await GetTokenDto(user, roles, registerDto.DeviceId));
        }
        public async Task<Result<TokenDto>> LoginUserAsync(LoginDto loginDto)
        {
            string unauthorizedMessage = "Username not found and/or password is incorrect!";

            // Check username
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user is null || !(await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false)).Succeeded)
                return Result<TokenDto>.Failure([unauthorizedMessage]);

            // Check password
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!signInResult.Succeeded)
                return Result<TokenDto>.Failure([unauthorizedMessage]);

            // Retrieve roles
            var roles = await _userManager.GetRolesAsync(user);

            return Result<TokenDto>.Success(await GetTokenDto(user, roles, loginDto.DeviceId));
        }
        public async Task<Result<TokenDto>> RefreshTokenAsync(TokenRequestDto tokenRequestDto)
        {
            // Get user from token
            var principal = _tokenService.GetPrincipalFromToken(tokenRequestDto.OldAccessToken);
            var userName = principal?.Identity?.Name;
            if (userName is null)
                return Result<TokenDto>.Failure(["Invalid token."]);

            // Find user in DB
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user is null)
                return Result<TokenDto>.Failure(["User not found."]);

            // Validate token
            var storedToken = await _refreshTokenRepo.GetByUserIdAndDeviceIdAsync(user.Id, tokenRequestDto.DeviceId);
            if (storedToken is null || !_tokenService.VerifyRefreshToken(tokenRequestDto.RefreshToken, storedToken.TokenHash))
                return Result<TokenDto>.Failure(["Invalid refresh token."]);

            // Retrieve user roles
            var roles = await _userManager.GetRolesAsync(user);

            return Result<TokenDto>.Success(await GetTokenDto(user, roles, tokenRequestDto.DeviceId));
        }
        private async Task<TokenDto> GetTokenDto(User user, IList<string> roles, string deviceId)
        {
            // Set token expiration time
            var accessTokenExpirationTime = DateTime.Now.AddMinutes(30);
            var refreshTokenExpirationTime = DateTime.Now.AddDays(30).ToUniversalTime();

            // Generate access token
            var accessToken = _tokenService.GenerateAccessToken(user, roles, accessTokenExpirationTime);
            (var refreshTokenDto, var refreshTokenString) = _tokenService.GenerateRefreshToken(user.Id, deviceId, refreshTokenExpirationTime);

            // Save new or update existing token for specified user device 
            await _refreshTokenRepo.UpdateOrCreateAsync(refreshTokenDto);

            // Create token dto
            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString
            };
        }
    }
}