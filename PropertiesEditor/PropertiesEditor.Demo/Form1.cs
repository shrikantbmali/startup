using PropertiesEditor.Attributes;
using System;
using System.Windows.Forms;

namespace PropertiesEditor.Demo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var type = typeof (DemoPropertyClass);

			object[] customAttributes = type.GetCustomAttributes(true);

			foreach (var customAttribute in customAttributes)
			{
				Console.WriteLine(customAttribute.GetType().FullName);
			}
		}
	}

	[PropertiesOwner]
	internal class DemoPropertyClass
	{
		[Property("This is dummy Title")]
		public string Dummy { get; set; }
	}
}