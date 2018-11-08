using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Todoist
{
    public class PlaceholderBehaviour : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;

            AssociatedObject.Text = Placeholder;
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AssociatedObject.Text))
            {
                AssociatedObject.Text = Placeholder;
            }
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Placeholder.Equals(AssociatedObject.Text))
            {
                AssociatedObject.Text = string.Empty;
            }
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder", typeof (string), typeof (PlaceholderBehaviour), new PropertyMetadata(default(string)));

        public string Placeholder
        {
            get { return (string) GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
    }
}