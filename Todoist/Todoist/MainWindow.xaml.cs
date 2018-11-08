using System.Windows;
using System.Windows.Forms;

namespace Todoist
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MouseLeftButtonDown += (sender, args) => DragMove();
            const int LeftTopMargin = 5;

            Left = Screen.PrimaryScreen.Bounds.Width - Width - LeftTopMargin;
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height - LeftTopMargin;
        }
    }
}