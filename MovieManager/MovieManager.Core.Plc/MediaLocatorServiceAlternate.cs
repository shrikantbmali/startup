using MovieManager.StructureModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieManager.Core
{
    internal class MediaLocatorServiceAlternate : MediaLocatorServiceBase
    {
        private string[] _fileExtensions =
        {
            ".avi", ".mp4", ".mkv", "divx", ".wmv", "m4v", "mpeg", "mpg", "h264", ".mov", ".vob", "mpeg", "mpg", "3gp", "3g2",
            "3gpp", "ogv"
        };

        private readonly Dictionary<long, MediaLocation> _mediaLocations;

        public MediaLocatorServiceAlternate()
        {
            _mediaLocations = new Dictionary<long, MediaLocation>();
        }

        public override void AddLocation(MediaLocation mediaLocation, bool beginMediaItemFetching)
        {
            mediaLocation = mediaLocation.Clone();

            if (mediaLocation.Id < 0)
                throw new ArgumentException("Id cannot be undefinned.");

            _mediaLocations.Add(mediaLocation.Id, mediaLocation);

            if (beginMediaItemFetching)
                FetchMediaFiles(mediaLocation.Path);
        }

        public override void RemoveLocation(MediaLocation mediaLocation)
        {
        }

        public override void UpdateLocation(MediaLocation updatedMediaLocation)
        {
            if (_mediaLocations.ContainsKey(updatedMediaLocation.Id))
            {
                var storedLocation = _mediaLocations[updatedMediaLocation.Id];

                if (!updatedMediaLocation.Equals(storedLocation))
                {
                    RemoveLocation(storedLocation);
                    AddLocation(updatedMediaLocation, false);
                }
            }
            else
                throw new InvalidOperationException("Cannot update media location which is not added.");
        }

        private bool IsMediaFile(string entry)
        {
            return _fileExtensions.Any(s => entry.EndsWith(s, StringComparison.CurrentCultureIgnoreCase));
        }

        private void FetchMediaFiles(string path)
        {
            try
            {
                foreach (var entry in Directory.EnumerateFileSystemEntries(path))
                {
                    if (Directory.Exists(entry))
                    {
                        // If it's a Directory and it exists, also checks if we have sufficient permissions.
                        FetchMediaFiles(entry);
                    }
                    else if (IsMediaFile(entry))
                    {
                        RaiseMediaFileFoundEvent(entry);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public override void Dispose()
        {
            _fileExtensions = null;
        }
    }
}