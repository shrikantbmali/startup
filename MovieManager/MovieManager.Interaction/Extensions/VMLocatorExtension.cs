using System.Mvvm;

namespace MovieManager.Interaction.Extensions
{
	internal static class VMLocatorExtension
	{
		public static void RegisterAllVMs(this IVMLocator vmLocator)
		{
			vmLocator.RegisterType<IAddRemoveFolderVM, AddRemoveFolderVM>();
			vmLocator.RegisterType<IFirstTimeViewModel, AddRemoveFolderVM>();
			vmLocator.RegisterType<IMainVM, MainVM>();
			vmLocator.RegisterType<StatusVM, StatusVM>();
		}
	}
}