namespace Calibr8Fit.Api.Services.Results
{
    public class Result<T>
    {
        public bool Succeeded { get; private set; }
        public T? Data { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }

        public static Result<T> Success(T data) => new() { Succeeded = true, Data = data };
        public static Result<T> Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors };
    }
}