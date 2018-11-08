using MovieManager.Core;
using MovieManager.Core.Settings;
using MovieManager.StructureModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Common.Dialogs;
using System.Infrastructure.Services;
using System.Linq;
using System.Mvvm;
using System.Windows.Input;

namespace MovieManager.Interaction
{
    internal class AddRemoveFolderVM : BindableBase, IAddRemoveFolderVM, IFirstTimeViewModel
    {
        private IMessengerService _messengerService;
        private IMediaCollectionManager _collectionManager;
        private readonly IApplicationSettings _applicationSettings;

        public ObservableCollection<MediaLocation> Locations { get; private set; }

        public ICommand AddLocationCommand { get; private set; }

        public ICommand RemoveLocationCommand { get; private set; }

        public ICommand OnLoadCommand { get; private set; }

        public StatusVM Status { get; private set; }

        public AddRemoveFolderVM(IVMLocator vmLocator, IMessengerService messengerService, IMediaCollectionManager collectionManager, IApplicationSettings applicationSettings)
            : base(vmLocator)
        {
            _collectionManager = collectionManager;
            _messengerService = messengerService;
            _applicationSettings = applicationSettings;
        }

        public void Load()
        {
            Status = VMLocator.GetViewModel<StatusVM>();
            Status.StatusString = "Please add a location to look for movies.";

            AddLocationCommand = new RelayCommand(AddLocationCmd);
            RemoveLocationCommand = new RelayCommand<long>(RemoveLocationCmd, i => true);

            Locations = new ObservableCollection<MediaLocation>();
            Locations.CollectionChanged += Locations_CollectionChanged;

            if (_collectionManager.IsAnyLocationAdded())
                foreach (var mediaLocation in _collectionManager.GetAllMovieLocations())
                    AddLocation(mediaLocation);
        }

        private void AddLocation(MediaLocation mediaLocation)
        {
            var addLocationResult = _collectionManager.AddMediaLocation(mediaLocation);

            if (addLocationResult.IsSuccessful)
            {
                Locations.Add(addLocationResult.Value);
            }
            else
            {
                _messengerService.ShowMessageBox(new MessageBoxData
                {
                    Title = "Error adding media location!",
                    Message = GetErrorString(addLocationResult.FailureReason),
                    MessageBoxButtons = MessageBoxButtons.OK,
                    MesageBoxIcon = MessageBoxIcon.Error
                });
            }
        }

        private void RemoveLocation(long id)
        {
            var location = Locations.FirstOrDefault(movieLocation => movieLocation.Id == id);

            if (location != null && Locations.Contains(location))
            {
                Locations.Remove(location);
                _collectionManager.DeleteMediaLocation(location);
            }
        }

        private static string GetErrorString(MediaLocationErrors failureReason)
        {
            switch (failureReason)
            {
                case MediaLocationErrors.LocationNotExitOnDrive:
                    return "Cannot add media location as it does not exits!";

                case MediaLocationErrors.AlreadyAdded:
                    return "Cannot add media location as it already added!";

                default:
                    throw new ArgumentOutOfRangeException("failureReason");
            }
        }

        #region Command Handler

        private void AddLocationCmd(object obj)
        {
            var result = _messengerService.ShowSelectFolderDialog("Please select a folder!");

            if (result.IsSuccessful)
            {
                var mediaLocation = new MediaLocation
                {
                    Path = result.Value,
                    IsToMonitor = true
                };

                AddLocation(mediaLocation);
            }
        }

        private void RemoveLocationCmd(long id)
        {
            var showMessageBox = _messengerService.ShowMessageBox(new MessageBoxData
            {
                Title = "Warning!",
                Message = "Are you sure to remove this movie lcation?" + Environment.NewLine +
                          "We will not be able to show any movie from this location.",
                MessageBoxButtons = MessageBoxButtons.OKCancel,
                MesageBoxIcon = MessageBoxIcon.Question
            });

            if (showMessageBox == DialogResult.OK)
            {
                RemoveLocation(id);
            }
        }

        #endregion Command Handler

        protected override void OnClose(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.OK)
            {
                foreach (var location in Locations)
                    _collectionManager.Update(location);

                _applicationSettings.IsMediaLocationSetup = true;
                _applicationSettings.IsDefaulSettings = true;

                _applicationSettings.Save();
            }

            Cleanup();

            base.OnClose(dialogResult);
        }

        private void Locations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseCollectionChangedEvent(e);
        }

        private void Cleanup()
        {
            Locations.CollectionChanged -= Locations_CollectionChanged;
            Locations.Clear();

            AddLocationCommand = null;
            RemoveLocationCommand = null;
            Status = null;

            _messengerService = null;
            _collectionManager = null;
        }
    }
}