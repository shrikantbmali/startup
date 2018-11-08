using System.Common.EventArguments;

namespace System.Common
{
    public interface IProcess
    {
        event EventHandler Started;

        event EventHandler<ProcessFailedEventArgs> Failed;

        event EventHandler Stopped;

        event EventHandler Completed;

        void Start();

        void Pause();

        void Stop();
    }
}