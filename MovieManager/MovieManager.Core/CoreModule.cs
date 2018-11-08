using Microsoft.Practices.Unity;
using MovieManager.ContextModel;
using MovieManager.Core.Settings;

namespace MovieManager.Core
{
	public class CoreModule
	{
		public void Intialize(IUnityContainer unityContainer)
		{
			new ContextModelModule().Intialize(unityContainer);

			unityContainer.RegisterInstance<IMediaLocatorService>(new MediaLocatorServiceAlternate());

			unityContainer.RegisterInstance<IMediaCollection>(
				new MediaCollection(unityContainer.Resolve<IMediaLocatorService>(),
					unityContainer.Resolve<IMediaLocationContext>(), unityContainer.Resolve<IMediaItemContext>()));

			unityContainer.RegisterInstance<IApplicationSettings>(new ApplicationSettings(unityContainer.Resolve<IApplicationSettingsContext>()));
		}
	}
}