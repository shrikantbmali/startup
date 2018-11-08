namespace FakeDocter.PrinteService
{
	public static class PrintServiceFactory
	{
		public static IVisualPrinter CreateXpsVisualPrinter()
		{
			return new XpsVisualPrinter();
		}
	}
}
