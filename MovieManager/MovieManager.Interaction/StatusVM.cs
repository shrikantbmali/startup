using System.Mvvm;

namespace MovieManager.Interaction
{
	public class StatusVM : BindableBase
	{
		public bool IsIndeterminate
		{
			get { return GetValue<bool>(); }
			set { SetValue(value); }
		}

		public double ProgressPercentage
		{
			get { return GetValue<double>(); }
			set { SetValue(value); }
		}

		public string StatusString
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}
	}
}