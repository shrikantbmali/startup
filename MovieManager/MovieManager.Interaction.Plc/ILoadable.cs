using System.Windows.Input;

namespace MovieManager.Interaction
{
    public interface ILoadable
    {
        ICommand OnLoadCommand { get; }

        void Load();
    }
}