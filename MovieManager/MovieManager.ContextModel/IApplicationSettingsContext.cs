namespace MovieManager.ContextModel
{
	public interface IApplicationSettingsContext
	{
		bool IsMediaLocationSetup { get; set; }

		bool IsDefaultSettings { get; set; }

		void Save();
	}
}