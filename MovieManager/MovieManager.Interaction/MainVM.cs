using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Mvvm;
using System.Windows.Input;
using MovieManager.Core;
using MovieManager.StructureModel;

namespace MovieManager.Interaction
{
	internal class MainVM : BindableBase, IMainVM
	{
		private readonly IMediaCollection _mediaCollectionManager;

		public ObservableCollection<MediaItem> AllItems { get; }

		public ICommand PlayCommand { get; }

		public ICommand OnLoadCommand { get; }

		public MainVM(IVMLocator vmLocator, IMediaCollection mediaCollectionManager)
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
				Process.Start(mediaItem.Path);
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