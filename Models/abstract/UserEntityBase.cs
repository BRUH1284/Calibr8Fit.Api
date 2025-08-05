using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public abstract class UserEntityBase<TKey>() : EntityBase<TKey>, IUserEntity
        where TKey : notnull
    {
        public required string UserId { get; set; }
    }
}