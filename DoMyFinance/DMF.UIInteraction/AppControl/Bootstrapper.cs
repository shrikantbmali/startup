using System;

namespace DMF.UIInteraction.AppControl
{
	public class Bootstrapper : IBootstrapper
	{
		private static readonly Lazy<IBootstrapper> _lazyInstance = new Lazy<IBootstrapper>(() => new Bootstrapper());

		public static IBootstrapper Instance
		{
			get { return _lazyInstance.Value; }
		}

		private Bootstrapper()
		{
		}

		public AppExecutionFlow GetAppExecutionFlow()
		{
			return new MainAppExeFlow(null);
		}
	}
}