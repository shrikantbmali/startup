using MovieManager.StructureModel;

namespace MovieManager.ContextModel.Data.Linq
{
	public static class MediaLocationConverters
	{
		public static Tbl_MediaLocation ConvertToTable(this MediaLocation location)
		{
			return new Tbl_MediaLocation().Morf(location);
		}

		public static MediaLocation ToActual(this Tbl_MediaLocation location)
		{
			return new MediaLocation(location.Id) { Path = location.Path, IsToMonitor = location.IsToMonitor };
		}

		public static Tbl_MediaLocation Morf(this Tbl_MediaLocation mediaLocation, MediaLocation location)
		{
			mediaLocation.Id = location.Id;
			mediaLocation.Path = location.Path;
			mediaLocation.IsToMonitor = location.IsToMonitor;

			return mediaLocation;
		}
	}
}