using Calibr8Fit.Api.DataTransferObjects.PushToken;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IPushService
    {
        Task RegisterPushToken(PushTokenDto pushTokenDto, string userId);
        Task PushNotificationAsync(string userId, string title, string body, string? imageUrl = null);
    }
}