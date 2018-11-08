using System.Printing;
using System.Windows.Documents.Serialization;
using System.Windows.Media;

namespace FakeDocter.PrinteService
{
	internal abstract class VisualPrinter : IVisualPrinter
	{
		public event WritingCancelledEventHandler WritingCancelled;

		public event WritingCompletedEventHandler WritingCompleted;

		public event WritingProgressChangedEventHandler WritingProgressChanged;

		public event WritingPrintTicketRequiredEventHandler WritingPrintTicketRequired;

		public abstract void Print(PrintQueue printQueue, Visual visual);

		protected virtual void RaiseWritingCancelledEvent(WritingCancelledEventArgs e)
		{
			var handler = WritingCancelled;

			if (handler != null)
				handler(this, e);
		}

		protected virtual void RaiseWritingCompletedEvent(WritingCompletedEventArgs e)
		{
			var handler = WritingCompleted;

			if (handler != null)
				handler(this, e);
		}

		protected virtual void RaiseWritingProgressChangedEvent(WritingProgressChangedEventArgs e)
		{
			var handler = WritingProgressChanged;

			if (handler != null)
				handler(this, e);
		}

		protected virtual void RaiseWritingPrintTicketRequiredEvent(WritingPrintTicketRequiredEventArgs e)
		{
			var handler = WritingPrintTicketRequired;

			if (handler != null)
				handler(this, e);
		}
	}
}