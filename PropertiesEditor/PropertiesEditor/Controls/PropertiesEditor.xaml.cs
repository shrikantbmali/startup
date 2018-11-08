using PropertiesEditor.Attributes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace PropertiesEditor.Controls
{
	public partial class PropertiesEditor
	{
		public static readonly DependencyProperty PropertyOwnerTypeProperty = DependencyProperty.Register(
			"PropertyOwnerType", typeof(Type), typeof(PropertiesEditor), new PropertyMetadata(default(Type)));

		public Type PropertyOwnerType
		{
			get { return (Type)GetValue(PropertyOwnerTypeProperty); }
			set { SetValue(PropertyOwnerTypeProperty, value); }
		}

		public PropertiesEditor()
		{
			InitializeComponent();

			Loaded += PropertiesEditor_Loaded;
			Unloaded += PropertiesEditor_Unloaded;
		}

		private void PropertiesEditor_Loaded(object sender, RoutedEventArgs e)
		{
			var attributeInfoReader = new AttributeInfoReader(PropertyOwnerType);

			var readAttributes = attributeInfoReader.ReadAttributes();

			foreach (IDisplayInfo attribute in readAttributes)
			{
				StackPanel.Children.Add(attribute.ToUIElement());
			}
		}

		private void PropertiesEditor_Unloaded(object sender, RoutedEventArgs e)
		{
			Loaded -= PropertiesEditor_Loaded;
			Unloaded -= PropertiesEditor_Unloaded;
		}
	}

	public static class DisplayInfoToUIElementConverter
	{
		private static readonly Dictionary<ControlType, Func<IDisplayInfo, UIElement>> _uiElementRetrievers =
			new Dictionary<ControlType, Func<IDisplayInfo, UIElement>>();

		static DisplayInfoToUIElementConverter()
		{
			_uiElementRetrievers.Add(ControlType.Text, GetTextBlock);
			_uiElementRetrievers.Add(ControlType.ComboBox, GetComboBox);
		}

		private static UIElement GetComboBox(IDisplayInfo displayInfo)
		{
			var comboBox = new ComboBox
			{
				IsEnabled = displayInfo.IsWritable
			};


			if (displayInfo.ChoiceElements != null)
				foreach (var choiceElement in displayInfo.ChoiceElements)
				{
					comboBox.Items.Add(new ListBoxItem() { Content = choiceElement });
				}

			return comboBox; 
		}

		private static UIElement GetTextBlock(IDisplayInfo displayInfo)
		{
			return new TextBlock
			{
				Text = displayInfo.Title + " " + displayInfo.ControlType.ProgrammaticName,
				IsEnabled = displayInfo.IsWritable
			};
		}

		public static UIElement ToUIElement(this IDisplayInfo displayInfo)
		{
			return _uiElementRetrievers[displayInfo.ControlType](displayInfo);
		}
	}
}