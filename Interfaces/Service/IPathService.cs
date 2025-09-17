namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IPathService
    {
        string GetUserUploadsDirectoryPath(string username);
        string GetProfilePictureDirectoryPath(string username);
        string? GetProfilePicturePath(string username, string fileName);
        string? GetProfilePictureUrl(HttpRequest httpRequest, string username, string fileName);
        string GetPostImagesDirectoryPath(string username, int postId);
        string? GetPostImagePath(string username, int postId, string fileName);
        string? GetPostImageUrl(HttpRequest httpRequest, string username, int postId, string fileName);
        string RemoveRoot(string path);
        string AddRoot(string path);
        string BuildPublicUrl(HttpRequest request, string relativePath);
        string? PublicUrlToRelativePath(string url);
    }
}