using Microsoft.Practices.Unity;
using MovieManager.Core.Settings;
using MovieManager.Interaction.Extensions;
using System;
using System.Common;
using System.Common.Dialogs;
using System.Infrastructure.Services;
using System.Mvvm;

namespace MovieManager.Interaction
{
    internal sealed class App : ProcessBase, IApp
    {
        private readonly IMessengerService _messengerService;
        private readonly IVMLocator _vmLocator;
        private readonly IApplicationSettings _applicationSettings;
        private readonly IUnityContainer _unityContainer;

        public App(IMessengerService messengerService, IUnityContainer unityContainer, IVMLocator vmLocator,
            IApplicationSettings applicationSettings)
        {
            _messengerService = messengerService;
            _unityContainer = unityContainer;
            _vmLocator = vmLocator;
            _applicationSettings = applicationSettings;
        }

        public override void Start()
        {
            _vmLocator.RegisterAllVMs();

            _unityContainer.RegisterInstance(_messengerService);

            InitializeApp();
        }

        private void InitializeApp()
        {
            if (!_applicationSettings.IsMediaLocationSetup)
            {
                var firstTimeViewModel = _vmLocator.GetViewModel<IFirstTimeViewModel>();

                firstTimeViewModel.Load();

                //Info: This should Start the application main window.
                RaiseStartedEvent();

                firstTimeViewModel.Closed += firstTimeViewModel_Closed;
                _messengerService.ShowView<IFirstTimeViewModel>(firstTimeViewModel);
            }
            else
            {
                var mainVM = GetMainView();

                RaiseStartedEvent();

                _messengerService.ShowView<IMainVM>(mainVM);
            }
        }

        private IMainVM GetMainView()
        {
            var mainVM = _vmLocator.GetViewModel<IMainVM>();
            mainVM.Closed += (sender, result) => _messengerService.CloseView<IMainVM>();
            return mainVM;
        }

        private void firstTimeViewModel_Closed(IBindable sender, DialogResult e)
        {
            var addRemoveFolderVM = sender as IFirstTimeViewModel;
            if (addRemoveFolderVM == null)
                throw new ArgumentException("The sender cannot be null, here the sender should be coverted back to it's ViewModel.");

            addRemoveFolderVM.Closed -= firstTimeViewModel_Closed;

            addRemoveFolderVM.Dispose();

            _messengerService.CloseView<IFirstTimeViewModel>();
            _messengerService.ShowView<IMainVM>(GetMainView());
        }
    }
}