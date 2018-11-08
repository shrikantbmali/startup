namespace System.Common
{
    public interface IResult<out TResultType>
    {
        bool IsSuccessful { get; }

        TResultType Value { get; }

        string Message { get; }
    }

    public interface IResult<out TResultType, out TFailureReason> : IResult<TResultType>
    {
        TFailureReason FailureReason { get; }
    }
}