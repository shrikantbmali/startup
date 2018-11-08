using PropertiesEditor.Attributes;
using System.Windows.Automation;

namespace PropertiesEditor.Controls
{
	public struct DisplayInfo : IDisplayInfo
	{
		public ControlType ControlType { get; private set; }

		public bool IsWritable { get; private set; }

		public string Title { get; private set; }

		public string[] ChoiceElements { get; set; }

		public DisplayInfo(string title, ControlType controlType)
			: this()
		{
			ControlType = controlType;
			Title = title;
		}

		public DisplayInfo(string title, ControlType controlType, bool isWritable)
			: this(title, controlType)
		{
			IsWritable = isWritable;
		}
	}
}