using System;
using System.Common;

namespace MovieManager.StructureModel
{
	public sealed class MediaLocation : IIdentifiable<long>, IEquatable<MediaLocation>, ICloneableT<MediaLocation>
	{
		public long Id { get; }

		public string Path { get; set; }

		public bool IsToMonitor { get; set; }

		public MediaLocation()
		{
			Id = -1;
		}

		public MediaLocation(long id)
		{
			Id = id;
		}

		public bool Equals(MediaLocation other)
		{
			return Id == other.Id && Path.Equals(other.Path, StringComparison.CurrentCultureIgnoreCase) &&
				   IsToMonitor == other.IsToMonitor;
		}

		public MediaLocation Clone()
		{
			return new MediaLocation(Id)
			{
				Path = Path,
				IsToMonitor = IsToMonitor
			};
		}
	}
}