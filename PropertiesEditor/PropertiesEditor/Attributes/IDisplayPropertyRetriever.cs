using System.Collections.Generic;

namespace PropertiesEditor.Attributes
{
	public interface IDisplayPropertyRetriever
	{
		IEnumerable<string> GetDisplayInfos(object associationObject);
	}
}