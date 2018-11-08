namespace System.Common
{
    public class UserLoginSession : IUserLoginSession
    {
        public UserLoginSession(string sessionID, long id, TimeSpan sessionTimeout)
        {
            SessionID = sessionID;
            Id = id;

            SessionTimeout = sessionTimeout;
        }

        public DateTime SessionStart { get; private set; }

        public TimeSpan SessionTimeout { get; }

        public string SessionID { get; }

        public long Id { get; }
    }
}