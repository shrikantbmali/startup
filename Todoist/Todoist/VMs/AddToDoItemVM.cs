using System.Mvvm;
using System.Windows.Input;
using Todoist.Views;

namespace Todoist.VMs
{
    public class AddToDoItemVM : BindableBase
    {
        public ICommand AddTodoItemCmd { get; private set; }

        public AddToDoItemVM()
        {
            AddTodoItemCmd = new RelayCommand(AddTotoItem);
        }

        private static void AddTotoItem(object obj)
        {
            var finder = new Finder();
            finder.ShowDialog();
        }
    }
}