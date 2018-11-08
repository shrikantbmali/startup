using MovieManager.ContextModel;

namespace MovieManager.Core.Settings
{
    internal class ApplicationSettings : IApplicationSettings
    {
        private readonly IApplicationSettingsContext _appSettingsContext;

        public bool IsMediaLocationSetup
        {
            get { return _appSettingsContext.IsMediaLocationSetup; }
            set { _appSettingsContext.IsMediaLocationSetup = value; }
        }

        public bool IsDefaulSettings
        {
            get { return _appSettingsContext.IsDefaultSettings; }
            set { _appSettingsContext.IsDefaultSettings = value; }
        }

        public void Save()
        {
            _appSettingsContext.Save();
        }

        public ApplicationSettings(IApplicationSettingsContext appSettingsContext)
        {
            _appSettingsContext = appSettingsContext;
        }
    }
}