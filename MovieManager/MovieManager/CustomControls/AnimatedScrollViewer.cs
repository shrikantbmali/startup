using System.Windows;
using System.Windows.Controls;

namespace MovieManager.CustomControls
{
	public class AnimatedScrollViewer : ScrollViewer
	{
		static AnimatedScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedScrollViewer), new FrameworkPropertyMetadata(typeof(AnimatedScrollViewer)));
		}
	}
}