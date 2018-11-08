using System.Collections.Generic;
using System.Linq;
using MovieManager.ContextModel.Data;
using MovieManager.ContextModel.Data.Linq;
using MovieManager.StructureModel;

namespace MovieManager.ContextModel
{
	internal class MediaItemContext : Context<MediaItem, long>, IMediaItemContext
	{
		private static readonly LMDBDataContext Context = new LMDBDataContext();

		public override MediaItem Save(MediaItem context)
		{
			var tblMediaLocation = context.ConvertToTable();

			Context.Tbl_MediaItems.InsertOnSubmit(tblMediaLocation);

			Context.SubmitChanges();

			return tblMediaLocation.ToActual();
		}

		public override void Update(MediaItem context)
		{
			var mediaLocation = Context.Tbl_MediaItems.First(location => location.Id == context.Id);

			mediaLocation.Morf(context);
		}

		public override void Delete(MediaItem context)
		{
			var mediaLocation = Context.Tbl_MediaItems.SingleOrDefault(location => location.Id == context.Id);

			if (mediaLocation != null)
			{
				Context.Tbl_MediaItems.DeleteOnSubmit(mediaLocation);

				Context.SubmitChanges();
			}
		}

		public override MediaItem GetSpecific(long id)
		{
			return Context.Tbl_MediaItems.First(item => item.Id == id).ToActual();
		}

		public override IEnumerable<MediaItem> GetAllOf(long foreignKey)
		{
			return from tblMediaItem in Context.Tbl_MediaItems
				   where tblMediaItem.MediaLocationId == foreignKey
				   select tblMediaItem.ToActual();
		}

		public override IEnumerable<MediaItem> GetAll()
		{
			return from mediaItem in Context.Tbl_MediaItems select mediaItem.ToActual();
		}
	}
}