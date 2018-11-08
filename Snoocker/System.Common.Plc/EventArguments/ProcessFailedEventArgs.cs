namespace System.Common.EventArguments
{
    public class ProcessFailedEventArgs : EventArgs
    {
        public int ErrorNumber { get; private set; }

        public string ErrorMessage { get; private set; }

        public ProcessFailedEventArgs(int errorNumber, string errorMessage)
        {
            ErrorNumber = errorNumber;
            ErrorMessage = errorMessage;
        }
    }
}