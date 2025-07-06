namespace Todo.Common.Result
{
    public partial class Result<TValue> : Result
    {
        public TValue Value { get; }

        protected Result() : base()
        {
            Value = default;
        }

        protected Result(TValue value) : base()
        {
            Value = value;
        }

        private Result(Error error) : base(error)
        {
            Value = default;
        }

        public static Result<TValue> Success(TValue value) => new(value);


        public static implicit operator Result<TValue>(TValue value) => new(value);

        public static implicit operator Result<TValue>(Error error) => new(error);
    }
}
