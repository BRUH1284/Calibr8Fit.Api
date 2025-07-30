using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IDataChecksumProvider
    {
        Task<string> GenerateUserDataChecksumAsync(string userId);
        string GenerateChecksum<T>(T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var bytes = Encoding.UTF8.GetBytes(jsonData);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}