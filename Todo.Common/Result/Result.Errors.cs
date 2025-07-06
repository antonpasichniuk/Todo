namespace Todo.Common.Result
{
    public partial class Result
    {
        public static Error NotFound(string error) => new(error, ErrorType.NotFound);
        public static Error Validation(string error) => new(error, ErrorType.Validation);
        public static Error Authorization(string error) => new(error, ErrorType.Authorization);
    }
}
