using MovieManager.Core;
using MovieManager.StructureModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Mvvm;
using System.Windows.Input;

namespace MovieManager.Interaction
{
    internal class MainVM : BindableBase, IMainVM
    {
        private readonly IMediaCollectionManager _mediaCollectionManager;

        public ObservableCollection<MediaItem> AllItems { get; }

        public ICommand PlayCommand { get; }

        public ICommand OnLoadCommand { get; }

        public MainVM(IVMLocator vmLocator, IMediaCollectionManager mediaCollectionManager)
            : base(vmLocator)
        {
            _mediaCollectionManager = mediaCollectionManager;
            AllItems = new ObservableCollection<MediaItem>();
            AllItems.CollectionChanged += AllItems_CollectionChanged;

            OnLoadCommand = new RelayCommand(LoadedCmd);
            PlayCommand = new RelayCommand<long>(PlayCmd, l => true);
        }

        private void PlayCmd(long mediaItemId)
        {
            var mediaItem = AllItems.FirstOrDefault(item => item.Id == mediaItemId);

            if (mediaItem != null)
            {
                //Process.Start(mediaItem.Path);
            }
        }

        private void AllItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseCollectionChangedEvent(e);
        }

        private void LoadedCmd(object obj)
        {
            Load();
        }

        public void Load()
        {
            //await Task.Factory.StartNew(() =>
            //{
            foreach (var mediaItem in _mediaCollectionManager.GetAllMediaItems())
            {
                AllItems.Add(mediaItem);
            }
            //});
        }
    }
}