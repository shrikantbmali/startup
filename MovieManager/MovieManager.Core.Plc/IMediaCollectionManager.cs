using MovieManager.StructureModel;
using System;
using System.Collections.Generic;
using System.Common;

namespace MovieManager.Core
{
    public interface IMediaCollectionManager : IDisposable
    {
        IEnumerable<MediaLocation> GetAllMovieLocations();

        IResult<MediaLocation, MediaLocationErrors> AddMediaLocation(MediaLocation mediaLocation);

        void DeleteMediaLocation(MediaLocation mediaLocation);

        void Update(MediaLocation mediaLocation);

        bool IsAnyLocationAdded();

        IEnumerable<MediaItem> GetAllMediaItems();
    }
}