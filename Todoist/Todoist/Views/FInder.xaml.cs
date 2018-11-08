using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Todoist.Views
{
    public partial class Finder : Window
    {
        private readonly Storyboard _closeStoryBoard;

        public Finder()
        {
            InitializeComponent();

            PreviewKeyDown += Finder_PreviewKeyDown;
            Loaded += Finder_Loaded;

            _closeStoryBoard = FindResource("CloseStoryBoard") as Storyboard;

            if (_closeStoryBoard != null)
                _closeStoryBoard.Completed += (o, args) => Close();
        }

        private void Finder_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 60;)
            {
                CbxMibutes.Items.Add(i);
                i = i + 10;
            }
        }

        private void Finder_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                BeingClosingSequence();
            }
        }

        private void BeingClosingSequence()
        {
            _closeStoryBoard.Begin();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            BeingClosingSequence();
        }
    }
}