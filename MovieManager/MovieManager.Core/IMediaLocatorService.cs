using System;
using MovieManager.StructureModel;

namespace MovieManager.Core
{
	public interface IMediaLocatorService : IDisposable
	{
		void AddLocation(MediaLocation mediaLocation, bool beginMediaItemFetching);

		void RemoveLocation(MediaLocation mediaLocation);

		void UpdateLocation(MediaLocation updatedMediaLocation);

		event EventHandler<MediaItemEventArgs> MediaItemFound;

		event EventHandler<MediaItemEventArgs> MediaItemRemoved;

		event EventHandler<MediaItemEventArgs> MediaItemRenamed;
	}
}