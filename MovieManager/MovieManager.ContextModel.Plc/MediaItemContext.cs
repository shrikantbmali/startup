using MovieManager.StructureModel;
using System;
using System.Collections.Generic;

namespace MovieManager.ContextModel
{
    internal class MediaItemContext : Context<MediaItem, long>, IMediaItemContext
    {
        public override MediaItem Save(MediaItem context)
        {
            throw new NotImplementedException();
        }

        public override void Update(MediaItem context)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MediaItem context)
        {
            throw new NotImplementedException();
        }

        public override MediaItem GetSpecific(long id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MediaItem> GetAllOf(long foreignKey)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MediaItem> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}