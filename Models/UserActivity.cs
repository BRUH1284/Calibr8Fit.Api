namespace Calibr8Fit.Api.Models
{
    public class UserActivity : BaseActivity
    {
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public required DateTime UpdatedAt { get; set; }
    }
}