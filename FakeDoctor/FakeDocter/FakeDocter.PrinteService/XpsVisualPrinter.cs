using System.Printing;
using System.Windows.Media;

namespace FakeDocter.PrinteService
{
	internal class XpsVisualPrinter : VisualPrinter
	{
		public override void Print(PrintQueue printQueue, Visual visual)
		{
			var xpsDocumentWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
			xpsDocumentWriter.Write(visual);
		}
	}
}
