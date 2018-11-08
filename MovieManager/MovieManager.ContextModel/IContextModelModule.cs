using Microsoft.Practices.Unity;

namespace MovieManager.ContextModel
{
	public class ContextModelModule
	{
		public void Intialize(IUnityContainer unityContainer)
		{
			unityContainer.RegisterInstance<IMediaLocationContext>(new MediaLocationContext());
			unityContainer.RegisterInstance<IMediaItemContext>(new MediaItemContext());
			unityContainer.RegisterInstance<IApplicationSettingsContext>(new ApplicationSettingsContext());
		}
	}
}