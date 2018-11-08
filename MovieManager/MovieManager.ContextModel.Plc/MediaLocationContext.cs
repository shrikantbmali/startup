using MovieManager.StructureModel;
using System;
using System.Collections.Generic;

namespace MovieManager.ContextModel
{
    internal class MediaLocationContext : Context<MediaLocation, long>, IMediaLocationContext
    {
        public override MediaLocation Save(MediaLocation context)
        {
            throw new NotImplementedException();
        }

        public override void Update(MediaLocation context)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MediaLocation context)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MediaLocation> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsLocationAlreadyAdded(string location)
        {
            throw new NotImplementedException();
        }

        private static bool Equals(string objA, string objB)
        {
            throw new NotImplementedException();
        }
    }
}