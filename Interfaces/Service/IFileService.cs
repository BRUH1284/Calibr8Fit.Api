namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IFileService
    {
        bool IsImage(IFormFile file);
        bool IsExist(string path);
        Task<string> SaveImageAsync(IFormFile file, string savePath, string? fileName = null);
        void DeleteFile(string path);
        void DeleteDirectory(string path);
        Stream GetFileStream(string path);
        string GetContentType(string fileName);
    }
}