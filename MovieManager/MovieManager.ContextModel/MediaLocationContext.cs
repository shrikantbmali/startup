using System;
using System.Collections.Generic;
using System.Linq;
using MovieManager.ContextModel.Data;
using MovieManager.ContextModel.Data.Linq;
using MovieManager.StructureModel;

namespace MovieManager.ContextModel
{
	internal class MediaLocationContext : Context<MediaLocation, long>, IMediaLocationContext
	{
		private static readonly LMDBDataContext Context = new LMDBDataContext();

		public override MediaLocation Save(MediaLocation context)
		{
			var tblMediaLocation = context.ConvertToTable();

			Context.Tbl_MediaLocations.InsertOnSubmit(tblMediaLocation);

			Context.SubmitChanges();

			return tblMediaLocation.ToActual();
		}

		public override void Update(MediaLocation context)
		{
			var mediaLocation = Context.Tbl_MediaLocations.First(location => location.Id == context.Id);

			mediaLocation.Morf(context);

			Context.SubmitChanges();
		}

		public override void Delete(MediaLocation context)
		{
			var mediaLocation = Context.Tbl_MediaLocations.SingleOrDefault(location => location.Id == context.Id);

			if (mediaLocation != null)
			{
				var mediaItems = from item in Context.Tbl_MediaItems where item.MediaLocationId == context.Id select item;

				Context.Tbl_MediaItems.DeleteAllOnSubmit(mediaItems);
				Context.Tbl_MediaLocations.DeleteOnSubmit(mediaLocation);

				Context.SubmitChanges();
			}
		}

		public override IEnumerable<MediaLocation> GetAll()
		{
			return (from location in Context.Tbl_MediaLocations select location.ToActual());
		}

		public bool IsLocationAlreadyAdded(string location)
		{
			return Context.Tbl_MediaLocations.Any(mediaLocation => Equals(mediaLocation.Path, location));
		}

		private static bool Equals(string objA, string objB)
		{
			return objA.Equals(objB, StringComparison.CurrentCultureIgnoreCase);
		}
	}
}