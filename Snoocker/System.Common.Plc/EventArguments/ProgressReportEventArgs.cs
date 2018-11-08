namespace System.Common.EventArguments
{
    public class ProgressReportEventArgs : EventArgs
    {
        public double PercentCompleted { get; private set; }

        public ProgressReportEventArgs(double percentCompleted)
        {
            PercentCompleted = percentCompleted;
        }
    }
}