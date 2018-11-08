using System;
using System.Collections.Generic;
using System.Infrastructure;
using System.Infrastructure.Services;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using MovieManager.Interaction;
using MovieManager.Properties;
using MovieManager.Views;

namespace MovieManager
{
	internal class ViewsHander : IViewsHander
	{
		private IUnityContainer _unityContainer;
		private IShellInterface _shellInterface;
		private IContainer _viewsContainer;
		private Dictionary<Type, string> _regions = new Dictionary<Type, string>();

		public ViewsHander(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		public void Intialize()
		{
			_viewsContainer = _unityContainer.Resolve<IContainer>();

			RegisterViewsAndRegions(_viewsContainer);

			RegisterMessengerEventHandler(_unityContainer.Resolve<IMessengerService>());
		}

		public void Start()
		{
			_shellInterface = _unityContainer.Resolve<IShellInterface>();
			_shellInterface.ShowActivated = true;
			_shellInterface.Show();
		}

		private void RegisterMessengerEventHandler(IMessengerService messengerService)
		{
			messengerService.ApplicationShutDownRequested += messengerService_ApplicationShutDownRequested;
			messengerService.CloseViewRequested += messengerService_CloseViewRequested;
			messengerService.ShowChildDialog += messengerService_ShowChildDialog;
			messengerService.ShowStandAloneDialog += messengerService_ShowStandAloneDialog;
			messengerService.ShowViewRequested += messengerService_ShowViewRequested;
		}

		private void RegisterViewsAndRegions(IContainer viewContainer)
		{
			RegisterViewAndAssociateWithRegion<IAddRemoveFolderVM, FirstTimeView>(viewContainer, Resources.CenterDialog);
			RegisterViewAndAssociateWithRegion<IFirstTimeViewModel, FirstTimeView>(viewContainer, Resources.CenterDialog);
			RegisterViewAndAssociateWithRegion<IMainVM, MainView>(viewContainer, Resources.Fill);
		}

		private void RegisterViewAndAssociateWithRegion<TKeyType, TValueType>(IContainer viewContainer, string region)
		{
			var keyType = typeof(TKeyType);

			_regions.Add(keyType, region);

			viewContainer.AddMapping(keyType, typeof(TValueType));
		}

		private void messengerService_ShowViewRequested(object sender, VMOpenCloseEventArgs e)
		{
			var content = _viewsContainer.ResolveFor<UserControl>(e.ViewModelType);
			content.DataContext = e.DataContext;

			_shellInterface.SetContent(content, GetRegion(e));
		}

		private void messengerService_ShowStandAloneDialog(object sender, VMOpenCloseEventArgs e)
		{
			var view = _viewsContainer.ResolveFor<Window>(e.ViewModelType);
			view.DataContext = e.DataContext;

			view.ShowDialog();
		}

		private void messengerService_ShowChildDialog(object sender, ShowDialogViewMessengerEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void messengerService_CloseViewRequested(object sender, VMOpenCloseEventArgs e)
		{
			_shellInterface.ClearContent(GetRegion(e));
		}

		private void messengerService_ApplicationShutDownRequested(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private string GetRegion(VMOpenCloseEventArgs e)
		{
			return string.IsNullOrEmpty(e.Region) ? _regions[e.ViewModelType] : e.Region;
		}

		public void Dispose()
		{
			_unityContainer = null;
			_shellInterface = null;
			_viewsContainer = null;

			_regions.Clear();
			_regions = null;
		}
	}
}