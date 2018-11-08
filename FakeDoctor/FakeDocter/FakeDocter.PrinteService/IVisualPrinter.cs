using System.Printing;
using System.Windows.Documents.Serialization;
using System.Windows.Media;

namespace FakeDocter.PrinteService
{
    public interface IVisualPrinter
    {
	    void Print(PrintQueue printQueue, Visual visual);
		event WritingCancelledEventHandler WritingCancelled;
		event WritingCompletedEventHandler WritingCompleted;
		event WritingProgressChangedEventHandler WritingProgressChanged;
		event WritingPrintTicketRequiredEventHandler WritingPrintTicketRequired;
    }
}
