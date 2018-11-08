using System;
using MovieManager.StructureModel;

namespace MovieManager.Core
{
	public class MediaItemEventArgs : EventArgs
	{
		public string Path { get; private set; }

		public string OldPath { get; private set; }

		public MediaItemEventArgs(string path)
		{
			Path = path;
		}

		public MediaItemEventArgs(string path, string oldPath)
			: this(path)
		{
			OldPath = oldPath;
		}
	}

	public class MediaItemFoundEventArgs : EventArgs
	{
		public MediaItem MediaItem { get; private set; }

		public MediaItemFoundEventArgs(MediaItem mediaItem)
		{
			MediaItem = mediaItem;
		}
	}
}