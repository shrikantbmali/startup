using System.Collections.ObjectModel;
using System.Mvvm;
using System.Windows.Input;
using MovieManager.StructureModel;

namespace MovieManager.Interaction
{
	public interface IAddRemoveFolderVM : IBindable, ILoadable
	{
		ObservableCollection<MediaLocation> Locations { get; }

		ICommand AddLocationCommand { get; }

		StatusVM Status { get; }

		ICommand RemoveLocationCommand { get; }
	}
}