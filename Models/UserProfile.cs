namespace Calibr8Fit.Api.Models
{
    public class UserProfile
    {
        public required string UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}