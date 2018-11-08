using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace MovieManager
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var forCurrentView = ApplicationView.GetForCurrentView();
            forCurrentView.TryResizeView(new Size(12, 45));
            var applicationViewTitleBar = forCurrentView.TitleBar;

            applicationViewTitleBar.BackgroundColor = new Color { A = 255, R = 73, G = 142, B = 226 };
        }
    }
}