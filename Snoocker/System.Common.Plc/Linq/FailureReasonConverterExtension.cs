namespace System.Common.Linq
{
    public static class FailureReasonConverterExtension
    {
        public static UserAccountFailureReason ToUserAccountsFailureReason(this DBFailureReason dbFailureReason)
        {
            switch (dbFailureReason)
            {
                case DBFailureReason.None:
                    return UserAccountFailureReason.None;

                case DBFailureReason.CannotOpenAConnection:
                case DBFailureReason.SqlException:
                    return UserAccountFailureReason.ConnectionError;

                case DBFailureReason.InvalidDetails:
                    return UserAccountFailureReason.InvalidUserNameOrPassword;

                default:
                    throw new ArgumentOutOfRangeException("dbFailureReason");
            }
        }
    }
}