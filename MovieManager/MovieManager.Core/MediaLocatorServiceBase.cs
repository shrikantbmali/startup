using System;
using MovieManager.StructureModel;

namespace MovieManager.Core
{
	internal abstract class MediaLocatorServiceBase : IMediaLocatorService
	{
		public abstract void AddLocation(MediaLocation mediaLocation, bool beginMediaItemFetching);

		public abstract void RemoveLocation(MediaLocation mediaLocation);

		public abstract void UpdateLocation(MediaLocation updatedMediaLocation);

		#region Events

		public event EventHandler<MediaItemEventArgs> MediaItemFound;

		protected virtual void RaiseMediaFileFoundEvent(string path)
		{
			RaiseMediaFileFoundEvent(new MediaItemEventArgs(path));
		}

		protected virtual void RaiseMediaFileFoundEvent(MediaItemEventArgs e)
		{
			var handler = MediaItemFound;
			if (handler != null)
				handler(this, e);
		}

		public event EventHandler<MediaItemEventArgs> MediaItemRemoved;

		protected virtual void RaiseMediaFileRemoved(string path)
		{
			RaiseMediaFileRemoved(new MediaItemEventArgs(path));
		}

		protected virtual void RaiseMediaFileRemoved(MediaItemEventArgs e)
		{
			var handler = MediaItemRemoved;
			if (handler != null)
				handler(this, e);
		}

		public event EventHandler<MediaItemEventArgs> MediaItemRenamed;

		protected virtual void RaiseMediaFileRenamedEvent(MediaItemEventArgs e)
		{
			var handler = MediaItemRenamed;
			if (handler != null)
				handler(this, e);
		}

		protected virtual void RaiseMediaFileRenamedEvent(string path, string oldPath)
		{
			RaiseMediaFileRenamedEvent(new MediaItemEventArgs(path, oldPath));
		}

		#endregion Events

		public abstract void Dispose();
	}
}