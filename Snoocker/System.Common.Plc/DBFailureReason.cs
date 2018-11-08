namespace System.Common
{
    public enum DBFailureReason
    {
        None,
        CannotOpenAConnection,
        SqlException,
        InvalidDetails,
        IOException
    }
}