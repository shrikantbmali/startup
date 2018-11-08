using PropertiesEditor.Attributes;
using System.Collections.Generic;

namespace PropertiesEditor.WPFDemo
{
	[PropertiesOwner]
	internal class DemoPropertyClass : IDisplayPropertyRetriever
	{
		[Property("This is dummy Title 1")]
		public string Dummy { get; set; }

		[Property("This is dummy Title 2")]
		public int Integer { get; set; }

		[Property("This is dummy Title 3")]
		public bool Boolean { get; set; }

		[ListPropertyAttribute("This is dummy Title 4", typeof(EnumType))]
		public EnumType AnyEnumType { get; set; }

		public IEnumerable<string> GetDisplayInfos(object associationObject)
		{
			var displayInfo = new List<string>();

			if (associationObject.Equals(typeof(EnumType)))
			{
				displayInfo.Add("One String");
				displayInfo.Add("Two String");
				displayInfo.Add("Three String");
				displayInfo.Add("Four String");
			}

			return displayInfo;
		}
	}

	internal enum EnumType
	{
		One,
		Two,
		Three,
		Four
	}
}