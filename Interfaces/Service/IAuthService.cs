using Calibr8Fit.Api.DataTransferObjects.Authentication;
using Calibr8Fit.Api.DataTransferObjects.Token;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IAuthService
    {
        Task<IdentityResult<TokenDto>> RegisterUserAsync(RegisterDto registerDto);
        Task<Result<TokenDto>> LoginUserAsync(LoginDto loginDto);
        Task<Result<TokenDto>> RefreshTokenAsync(TokenRequestDto refreshTokenDto);
        Task<Result> LogoutAsync(string userId, string deviceId);
        Task<Result> LogoutAllAsync(string userId);
    }
}