using MovieManager.StructureModel;

namespace MovieManager.Interaction.StructureModel.Decors
{
	public class MediaItemDecor : MediaItem
	{
		public MediaItemDecor(MediaItem mediaItem)
			: base(mediaItem.Id, mediaItem.MediaLocationId)
		{
			Name = mediaItem.Name;
			Path = mediaItem.Path;
		}
	}
}