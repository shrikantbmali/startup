using MovieManager.ContextModel;
using MovieManager.StructureModel;
using System;
using System.Collections.Generic;
using System.Common;
using System.IO;
using System.Linq;

namespace MovieManager.Core
{
    public class MediaCollectionManager : IMediaCollectionManager
    {
        private IMediaLocatorService _mediaLocatorService;
        private IMediaLocationContext _mediaLocationContext;
        private IMediaItemContext _mediaItemContext;

        public MediaCollectionManager(IMediaLocatorService mediaLocatorService, IMediaLocationContext mediaLocationContext,
            IMediaItemContext mediaItemContext)
        {
            _mediaLocationContext = mediaLocationContext;
            _mediaItemContext = mediaItemContext;

            _mediaLocatorService = mediaLocatorService;

            _mediaLocatorService.MediaItemFound += _mediaLocatorService_MediaItemFound;
            _mediaLocatorService.MediaItemRemoved += _mediaLocatorService_MediaItemRemoved;
            _mediaLocatorService.MediaItemRenamed += _mediaLocatorService_MediaItemRenamed;
        }

        public IEnumerable<MediaLocation> GetAllMovieLocations()
        {
            return _mediaLocationContext.GetAll();
        }

        public IResult<MediaLocation, MediaLocationErrors> AddMediaLocation(MediaLocation mediaLocation)
        {
            MediaLocation location = null;
            var isSuccessful = true;

            var validationResult = ValidateMediaLocation(mediaLocation);

            switch (validationResult)
            {
                case MediaLocationErrors.AlreadyAdded:
                    _mediaLocatorService.AddLocation(mediaLocation, false);
                    location = mediaLocation;
                    break;

                case MediaLocationErrors.None:
                    location = _mediaLocationContext.Save(mediaLocation);
                    _mediaLocatorService.AddLocation(location, true);
                    break;

                default:
                    isSuccessful = false;
                    break;
            }

            return new Result<MediaLocation, MediaLocationErrors>(location, validationResult, isSuccessful);
        }

        public void DeleteMediaLocation(MediaLocation mediaLocation)
        {
            _mediaLocationContext.Delete(mediaLocation);
            _mediaLocatorService.RemoveLocation(mediaLocation);
        }

        public void Update(MediaLocation mediaLocation)
        {
            _mediaLocationContext.Update(mediaLocation);
            _mediaLocatorService.UpdateLocation(mediaLocation);
        }

        public bool IsAnyLocationAdded()
        {
            return _mediaLocationContext.GetAll().Any();
        }

        public IEnumerable<MediaItem> GetAllMediaItems()
        {
            return _mediaItemContext.GetAll();
        }

        private MediaLocationErrors ValidateMediaLocation(MediaLocation mediaLocation)
        {
            MediaLocationErrors error;

            if (mediaLocation.Id <= 0 && _mediaLocationContext.IsLocationAlreadyAdded(mediaLocation.Path))
            {
                error = MediaLocationErrors.UnknownError;
            }
            else if (_mediaLocationContext.IsLocationAlreadyAdded(mediaLocation.Path))
            {
                error = MediaLocationErrors.AlreadyAdded;
            }
            else if (!IsExists(mediaLocation.Path))
            {
                error = MediaLocationErrors.LocationNotExitOnDrive;
            }
            else
            {
                error = MediaLocationErrors.None;
            }

            return error;
        }

        private static bool IsExists(string path)
        {
            bool isPresent;

            try
            {
                isPresent = Directory.Exists(path);
            }
            catch (Exception)
            {
                isPresent = false;
            }

            return isPresent;
        }

        #region Media Item events

        private void _mediaLocatorService_MediaItemRenamed(object sender, MediaItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _mediaLocatorService_MediaItemRemoved(object sender, MediaItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _mediaLocatorService_MediaItemFound(object sender, MediaItemEventArgs e)
        {
            _mediaItemContext.Save(new MediaItem(GetMediaLocationId(e.Path)) { Path = e.Path, Name = Path.GetFileName(e.Path) });
        }

        private long GetMediaLocationId(string path)
        {
            var directory = Path.GetDirectoryName(path);
            long mediaLocationId = -1;

            var mediaLocations = _mediaLocationContext.GetAll().ToArray();

            while (!string.IsNullOrEmpty(directory))
            {
                var mediaLocation =
                    mediaLocations.FirstOrDefault(
                        location => location.Path.Equals(directory, StringComparison.CurrentCultureIgnoreCase));
                if (mediaLocation != null)
                {
                    mediaLocationId = mediaLocation.Id;
                    break;
                }

                directory = Path.GetDirectoryName(directory);
            }

            return mediaLocationId;
        }

        #endregion Media Item events

        public void Dispose()
        {
            _mediaLocatorService.MediaItemFound -= _mediaLocatorService_MediaItemFound;
            _mediaLocatorService.MediaItemRemoved -= _mediaLocatorService_MediaItemRemoved;
            _mediaLocatorService.MediaItemRenamed -= _mediaLocatorService_MediaItemRenamed;

            _mediaLocatorService.Dispose();

            _mediaLocatorService = null;
            _mediaLocationContext = null;
            _mediaItemContext = null;
        }
    }
}