using System.Linq;
using MovieManager.ContextModel.Data;

namespace MovieManager.ContextModel
{
	internal class ApplicationSettingsContext : IApplicationSettingsContext
	{
		private readonly Tbl_ApplicationSetting _tblApplicationSetting;
		private readonly LMDBDataContext _lmdbDataContext;

		public bool IsMediaLocationSetup
		{
			get { return _tblApplicationSetting.IsMediaLocationSetup; }
			set { _tblApplicationSetting.IsMediaLocationSetup = value; }
		}

		public bool IsDefaultSettings
		{
			get { return _tblApplicationSetting.IsDefaultSettings != null && _tblApplicationSetting.IsDefaultSettings.Value; }
			set { _tblApplicationSetting.IsDefaultSettings = value; }
		}

		public void Save()
		{
			_lmdbDataContext.SubmitChanges();
		}

		public ApplicationSettingsContext()
		{
			_lmdbDataContext = new LMDBDataContext();
			_tblApplicationSetting = _lmdbDataContext.Tbl_ApplicationSettings.FirstOrDefault();

			if (_tblApplicationSetting == null)
			{
				AddDefaultApplicationSettings();

				_tblApplicationSetting = _lmdbDataContext.Tbl_ApplicationSettings.FirstOrDefault();
			}
		}

		private void AddDefaultApplicationSettings()
		{
			var tblApplicationSetting = GetDefaultAppSettings();

			Save(tblApplicationSetting);
		}

		private void Save(Tbl_ApplicationSetting tblApplicationSetting)
		{
			_lmdbDataContext.Tbl_ApplicationSettings.InsertOnSubmit(tblApplicationSetting);

			_lmdbDataContext.SubmitChanges();
		}

		private static Tbl_ApplicationSetting GetDefaultAppSettings()
		{
			var tblApplicationSetting = new Tbl_ApplicationSetting { IsMediaLocationSetup = false, IsDefaultSettings = true };

			return tblApplicationSetting;
		}
	}
}