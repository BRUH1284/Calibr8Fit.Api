using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Post
{
    public class CreatePostRequestDto
    {
        [Required]
        public required string Content { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}