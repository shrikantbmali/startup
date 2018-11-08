using System.Collections.Generic;
using System.Windows.Controls;

namespace MovieManager
{
	public partial class Shell : IShellInterface
	{
		private readonly Dictionary<string, ContentControl> _regions = new Dictionary<string, ContentControl>();

		public Shell()
		{
			InitializeComponent();

			_regions.Add(Properties.Resources.Fill, Fill);
			_regions.Add(Properties.Resources.CenterDialog, CenterDialog);
		}

		public void SetContent(object content, string region)
		{
			ClearContent(region);
			GetRegion(region).Content = content;
		}

		public void ClearContent(string region)
		{
			GetRegion(region).Content = null;
		}

		private ContentControl GetRegion(string region)
		{
			return _regions[region];
		}
	}
}