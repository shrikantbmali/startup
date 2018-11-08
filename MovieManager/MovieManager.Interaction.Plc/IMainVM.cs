using MovieManager.StructureModel;
using System.Collections.ObjectModel;
using System.Mvvm;
using System.Windows.Input;

namespace MovieManager.Interaction
{
    public interface IMainVM : IBindable, ILoadable
    {
        ObservableCollection<MediaItem> AllItems { get; }

        ICommand PlayCommand { get; }
    }
}