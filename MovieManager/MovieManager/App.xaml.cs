using System;
using System.Infrastructure;
using System.Infrastructure.Services;
using System.Windows;
using Microsoft.Practices.Unity;
using MovieManager.Interaction;

namespace MovieManager
{
	public partial class App
	{
		private IUnityContainer _unityContainer;
		private IViewsHander _viewsHander;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_unityContainer = new UnityContainer();

			Intialize(_unityContainer);

			_viewsHander = _unityContainer.Resolve<IViewsHander>();

			_viewsHander.Intialize();

			StartApplication();
		}

		private void Intialize(IUnityContainer unitContainer)
		{
			if (unitContainer == null)
				throw new ArgumentNullException("unitContainer");

			unitContainer.RegisterInstance<IContainer>(new IocContainer());
			unitContainer.RegisterInstance<IMessengerService>(new MessengerService());
			unitContainer.RegisterInstance<IShellInterface>(new Shell());
			unitContainer.RegisterInstance<IViewsHander>(new ViewsHander(unitContainer));
		}

		private void StartApplication()
		{
			var interactionModule = new InteractionModule();
			interactionModule.Intialize(_unityContainer);

			var bootstrapper = _unityContainer.Resolve<IApp>();
			bootstrapper.Started += bootstrapper_Started;

			bootstrapper.Start();
		}

		private void bootstrapper_Started(object sender, EventArgs e)
		{
			_unityContainer.Resolve<IViewsHander>().Start();
		}
	}
}