using System.Common.EventArguments;

namespace System.Common
{
    public abstract class AsyncProcessBase : ProcessBase, IAsyncProcess
    {
        public abstract void StartAsynch();

        public event EventHandler<ProgressReportEventArgs> ProgressChanged;

        protected virtual void RaiseProgressChangedEvent(ProgressReportEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null)
                handler(this, e);
        }
    }
}