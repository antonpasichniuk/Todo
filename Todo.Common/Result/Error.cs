namespace Todo.Common.Result
{
    public record Error(string Message, ErrorType Type, int Code = 0);
}
