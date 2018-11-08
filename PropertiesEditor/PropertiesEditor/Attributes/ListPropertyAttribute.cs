using System.Windows.Automation;

namespace PropertiesEditor.Attributes
{
	public class ListPropertyAttribute : PropertyAttribute
	{
		public object AssociationObject { get; private set; }

		public ListPropertyAttribute(string title, object associationObject)
			: base(title)
		{
			AssociationObject = associationObject;
		}

		public ListPropertyAttribute(string title, object associationObject, bool isWritable)
			: base(title, isWritable)
		{
			AssociationObject = associationObject;
		}

		public ListPropertyAttribute(string title, object associationObject, ControlType controlType, bool isWritable = false)
			: base(title, controlType, isWritable)
		{
			AssociationObject = associationObject;
		}
	}
}