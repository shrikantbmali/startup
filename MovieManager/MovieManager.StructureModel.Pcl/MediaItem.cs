using System.Common;

namespace MovieManager.StructureModel
{
    public class MediaItem : IIdentifiable<long>
    {
        public long Id { get; }

        public long MediaLocationId { get; private set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public MediaItem()
        {
            Id = -1;
            MediaLocationId = -1;
        }

        public MediaItem(long mediaLocationId)
        {
            MediaLocationId = mediaLocationId;
        }

        public MediaItem(long id, long mediaLocationId)
        {
            Id = id;
            MediaLocationId = mediaLocationId;
        }
    }
}