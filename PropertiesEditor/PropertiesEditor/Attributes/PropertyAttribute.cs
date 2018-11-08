using System;
using System.ComponentModel;
using System.Windows.Automation;

namespace PropertiesEditor.Attributes
{
	public class PropertyAttribute : Attribute, IDisplayInfo
	{
		private bool _isWritable = true;

		public bool IsWritable
		{
			get { return _isWritable; }
			private set { _isWritable = value; }
		}

		public string Title { get; private set; }

		public ControlType ControlType { get; protected set; }

		public string[] ChoiceElements { get; set; }

		public PropertyAttribute(string title)
		{
			Title = title;
		}

		public PropertyAttribute(string title, bool isWritable)
			: this(title)
		{
			IsWritable = isWritable;
		}

		public PropertyAttribute(string title, ControlType controlType, bool isWritable = false)
		{
			Title = title;
			ControlType = controlType;
		}
	}
}