namespace Calibr8Fit.Api.Services.Results
{
    public class Result
    {
        public bool Succeeded { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }

        public static Result Success() => new() { Succeeded = true };
        public static Result Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors };
        public static Result Failure(string error) => new() { Succeeded = false, Errors = [error] };
    }

    public class Result<T>
    {
        public bool Succeeded { get; private set; }
        public T? Data { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }

        public static Result<T> Success(T data) => new() { Succeeded = true, Data = data };
        public static Result<T> Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors };
        public static Result<T> Failure(string error) => new() { Succeeded = false, Errors = [error] };
    }
}