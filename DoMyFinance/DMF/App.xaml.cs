using System.Windows;
using DMF.UIInteraction.AppControl;

namespace DMF
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var appExecutionFlow = Bootstrapper.Instance.GetAppExecutionFlow();
			appExecutionFlow.Start();
		}
	}
}
