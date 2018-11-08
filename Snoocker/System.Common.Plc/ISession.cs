namespace System.Common
{
    public interface ISession
    {
        DateTime SessionStart { get; }

        TimeSpan SessionTimeout { get; }

        string SessionID { get; }
    }
}