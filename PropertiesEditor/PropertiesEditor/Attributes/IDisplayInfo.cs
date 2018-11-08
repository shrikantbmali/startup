using System.Windows.Automation;

namespace PropertiesEditor.Attributes
{
	public interface IDisplayInfo
	{
		bool IsWritable { get; }

		string Title { get; }

		ControlType ControlType { get; }

		string[] ChoiceElements { get; set; }
	}
}