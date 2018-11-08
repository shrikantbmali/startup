using MovieManager.StructureModel;

namespace MovieManager.ContextModel.Data.Linq
{
	public static class MediaItemConverters
	{
		public static Tbl_MediaItem ConvertToTable(this MediaItem location)
		{
			return new Tbl_MediaItem().Morf(location);
		}

		public static MediaItem ToActual(this Tbl_MediaItem dbMediaItem)
		{
			return new MediaItem(dbMediaItem.Id, dbMediaItem.MediaLocationId).Morf(dbMediaItem);
		}

		public static Tbl_MediaItem Morf(this Tbl_MediaItem dbMediaItem, MediaItem mediaItem)
		{
			dbMediaItem.Id = mediaItem.Id;
			dbMediaItem.MediaLocationId = mediaItem.MediaLocationId;
			dbMediaItem.Path = mediaItem.Path;
			dbMediaItem.Name = mediaItem.Name;

			return dbMediaItem;
		}

		public static MediaItem Morf(this MediaItem mediaItem, Tbl_MediaItem dbMediaItem)
		{
			mediaItem.Path = dbMediaItem.Path;
			mediaItem.Name = dbMediaItem.Name;

			return mediaItem;
		}
	}
}