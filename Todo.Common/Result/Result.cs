namespace Todo.Common.Result
{
    public partial class Result
    {
        public bool IsSuccess { get; protected set; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; private set; }

        protected Result()
        {
            IsSuccess = true;
            Error = default;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success() => new();

        public static Error Failure(string error, int code = 0) => new(error, ErrorType.Failure, code);

        public static implicit operator Result(Error error) => new(error);
    }
}
