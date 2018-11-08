using System.Common.EventArguments;

namespace System.Common
{
    public interface IAsyncProcess : IProcess
    {
        event EventHandler<ProgressReportEventArgs> ProgressChanged;
    }
}