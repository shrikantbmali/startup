using PropertiesEditor.Attributes;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace PropertiesEditor.Controls
{
	internal class AttributeInfoReader
	{
		private readonly Type _propertyOwnerType;

		private readonly Dictionary<Type, Func<object, IDisplayInfo>> _displayInfoReader = new Dictionary<Type, Func<object, IDisplayInfo>>();

		public AttributeInfoReader(Type propertyOwnerType)
		{
			_propertyOwnerType = propertyOwnerType;
			_displayInfoReader = new Dictionary<Type, Func<object, IDisplayInfo>>
			{
				{typeof (PropertyAttribute), GetSimplePropertyDisplayInfo},
				{typeof (ListPropertyAttribute), GetListPropertyDisplayInfo},
			};
		}

		private IDisplayInfo GetListPropertyDisplayInfo(object arg)
		{
			if (arg is ListPropertyAttribute)
			{
				var propertyAttribute = arg as ListPropertyAttribute;

				var methodInfo = _propertyOwnerType.GetMethod("GetDisplayInfos");

				var result = methodInfo.Invoke(Activator.CreateInstance(_propertyOwnerType, null), new[] { propertyAttribute.AssociationObject });

				var displayInfo = new DisplayInfo(propertyAttribute.Title, propertyAttribute.ControlType ?? ControlType.ComboBox,
					propertyAttribute.IsWritable);

				if (result is IEnumerable<string>)
				{
					var choiceElement = new List<string>();

					foreach (var value in result as IEnumerable<string>)
					{
						choiceElement.Add(value);
					}

					displayInfo.ChoiceElements = choiceElement.ToArray();
				}

				return displayInfo;
			}

			throw new ArgumentException("Cannot tent to non PropertyAttribute type!");
		}

		public IEnumerable<IDisplayInfo> ReadAttributes()
		{
			var displayInfos = new List<IDisplayInfo>();

			var propertyInfos = _propertyOwnerType.GetProperties();

			foreach (var propertyInfo in propertyInfos)
			{
				var customAttributes = propertyInfo.GetCustomAttributes(true);

				foreach (var attribute in customAttributes)
				{
					displayInfos.Add(_displayInfoReader[attribute.GetType()](attribute));
				}
			}

			return displayInfos;
		}

		private IDisplayInfo GetSimplePropertyDisplayInfo(object arg)
		{
			if (arg is PropertyAttribute)
			{
				var propertyAttribute = arg as PropertyAttribute;

				return new DisplayInfo(propertyAttribute.Title, propertyAttribute.ControlType ?? ControlType.Text,
					propertyAttribute.IsWritable);
			}

			throw new ArgumentException("Cannot tent to non PropertyAttribute type!");
		}
	}
}