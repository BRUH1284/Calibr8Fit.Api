using Microsoft.AspNetCore.Identity;

namespace Calibr8Fit.Api.Services.Results
{
    public class IdentityResult<T>
    {
        public bool Succeeded { get; private set; }
        public T? Data { get; private set; }
        public IEnumerable<IdentityError>? Errors { get; private set; }

        public static IdentityResult<T> Success(T data) => new() { Succeeded = true, Data = data };
        public static IdentityResult<T> Failure(IEnumerable<IdentityError> errors) => new() { Succeeded = false, Errors = errors };
    }
}