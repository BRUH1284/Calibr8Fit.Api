namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IPathService
    {
        string GetUserUploadsDirectoryPath(string username);
        string GetProfilePictureDirectoryPath(string username);
        string? GetProfilePicturePath(string username, string fileName);
        string? GetProfilePictureUrl(string username, string fileName);
        string GetPostImagesDirectoryPath(string username, int postId);
        string? GetPostImagePath(string username, int postId, string fileName);
        string? GetPostImageUrl(string username, int postId, string fileName);
        string RemoveRoot(string path);
        string AddRoot(string path);
        string BuildPublicUrl(string relativePath);
        string? PublicUrlToRelativePath(string url);
    }
}