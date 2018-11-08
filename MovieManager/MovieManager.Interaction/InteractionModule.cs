using System.Infrastructure.Services;
using System.Mvvm;
using Microsoft.Practices.Unity;
using MovieManager.Core;
using MovieManager.Core.Settings;

namespace MovieManager.Interaction
{
	public class InteractionModule
	{
		public void Intialize(IUnityContainer globalUnityConatiner)
		{
			var localUnitContainer = new UnityContainer();

			InitializeCoreModule(localUnitContainer);

			localUnitContainer.RegisterInstance<IVMLocator>(new UnityDependantVMLocator(localUnitContainer));

			globalUnityConatiner.RegisterInstance<IApp>(
				new App(globalUnityConatiner.Resolve<IMessengerService>(),
					localUnitContainer,
					localUnitContainer.Resolve<IVMLocator>(),
					localUnitContainer.Resolve<IApplicationSettings>()));
		}

		private static void InitializeCoreModule(IUnityContainer unityContainer)
		{
			var coreModule = new CoreModule();
			coreModule.Intialize(unityContainer);
		}
	}
}