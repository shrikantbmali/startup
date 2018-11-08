namespace MovieManager.Core.Settings
{
    public interface IApplicationSettings
    {
        bool IsMediaLocationSetup { get; set; }

        bool IsDefaulSettings { get; set; }

        void Save();
    }
}