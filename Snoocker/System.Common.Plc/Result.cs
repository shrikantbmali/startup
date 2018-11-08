namespace System.Common
{
    public struct Result<TResultType> : IResult<TResultType>
    {
        public bool IsSuccessful { get; }

        public TResultType Value { get; }

        public string Message { get; set; }

        public static readonly IResult<TResultType> False = new Result<TResultType>(default(TResultType), false);

        public Result(TResultType value, bool isSuccessful)
            : this()
        {
            Value = value;
            IsSuccessful = isSuccessful;
        }
    }

    public struct Result<TResultType, TFailureReason> : IResult<TResultType, TFailureReason>
    {
        public bool IsSuccessful { get; }

        public TResultType Value { get; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public TFailureReason FailureReason { get; }

        public Result(TFailureReason failureReason, bool isSuccessful = false)
            : this()
        {
            IsSuccessful = isSuccessful;
            FailureReason = failureReason;
        }

        public Result(TResultType value, bool isSuccessful)
            : this()
        {
            IsSuccessful = isSuccessful;
            Value = value;
        }

        public Result(TResultType value, TFailureReason failureReason, bool isSuccessful)
            : this()
        {
            IsSuccessful = isSuccessful;
            Value = value;
            FailureReason = failureReason;
        }
    }
}