using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MovieManager.CustomControls
{
	public class CustomStatusBar : Control
	{
		public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
			"ImageSource", typeof(ImageSource), typeof(CustomStatusBar), new PropertyMetadata(default(ImageSource)));

		public ImageSource ImageSource
		{
			get { return (ImageSource)GetValue(ImageSourceProperty); }
			set { SetValue(ImageSourceProperty, value); }
		}

		public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
			"Status", typeof(string), typeof(CustomStatusBar), new PropertyMetadata(default(string)));

		public string Status
		{
			get { return (string)GetValue(StatusProperty); }
			set { SetValue(StatusProperty, value); }
		}

		public static readonly DependencyProperty ProgressPercentageProperty = DependencyProperty.Register(
			"ProgressPercentage", typeof(double), typeof(CustomStatusBar), new PropertyMetadata(default(double)));

		public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
			"IsIndeterminate", typeof(bool), typeof(CustomStatusBar), new PropertyMetadata(default(bool)));

		public bool IsIndeterminate
		{
			get { return (bool)GetValue(IsIndeterminateProperty); }
			set { SetValue(IsIndeterminateProperty, value); }
		}

		public double ProgressPercentage
		{
			get { return (double)GetValue(ProgressPercentageProperty); }
			set { SetValue(ProgressPercentageProperty, value); }
		}

		static CustomStatusBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomStatusBar), new FrameworkPropertyMetadata(typeof(CustomStatusBar)));
		}
	}
}