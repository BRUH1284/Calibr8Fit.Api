using Microsoft.AspNetCore.Identity;

namespace Calibr8Fit.Api.Models
{
    public class User : IdentityUser
    {
        public virtual UserProfile? Profile { get; set; }
    }
}