using MovieManager.StructureModel;
using System.Collections.ObjectModel;
using System.Mvvm;
using System.Windows.Input;

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