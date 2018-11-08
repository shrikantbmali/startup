using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MovieManager.StructureModel;

namespace MovieManager.Core
{
	internal class MediaLocatorServiceAlternate : MediaLocatorServiceBase
	{
		private string[] _fileExtensions =
		{
			".avi", ".mp4", ".mkv", "divx", ".wmv", "m4v", "mpeg", "mpg", "h264", ".mov", ".vob", "mpeg", "mpg", "3gp", "3g2",
			"3gpp", "ogv"
		};

		private readonly Lazy<Dictionary<long, FileSystemWatcher>> _fileSystemWatchers =
			new Lazy<Dictionary<long, FileSystemWatcher>>(() => new Dictionary<long, FileSystemWatcher>());

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

			if (mediaLocation.IsToMonitor)
				AddMediaLocationWatcher(mediaLocation);

			if (beginMediaItemFetching)
				FetchMediaFiles(mediaLocation.Path);
		}

		public override void RemoveLocation(MediaLocation mediaLocation)
		{
			if (_mediaLocations.ContainsKey(mediaLocation.Id))
			{
				_mediaLocations.Remove(mediaLocation.Id);

				if (_fileSystemWatchers.Value.ContainsKey(mediaLocation.Id))
				{
					var fileSystemWatcher = _fileSystemWatchers.Value[mediaLocation.Id];

					_fileSystemWatchers.Value.Remove(mediaLocation.Id);

					DisposeFileSystemWatcher(ref fileSystemWatcher);
				}
			}
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

		private void AddMediaLocationWatcher(MediaLocation mediaLocation)
		{
			_fileSystemWatchers.Value.Add(mediaLocation.Id, CreateFileSystemWatcher(mediaLocation.Path));
		}

		private FileSystemWatcher CreateFileSystemWatcher(string path)
		{
			var fileSystemWatcher = new FileSystemWatcher { Path = path, IncludeSubdirectories = true };

			fileSystemWatcher.Error += fileSystemWatcher_Error;
			fileSystemWatcher.Created += fileSystemWatcher_Created;
			fileSystemWatcher.Renamed += fileSystemWatcher_Renamed;
			fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;

			return fileSystemWatcher;
		}

		private bool IsMediaFile(string entry)
		{
			return _fileExtensions.Any(s => entry.EndsWith(s, StringComparison.InvariantCultureIgnoreCase));
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

		#region File System Watchers

		private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			if (IsMediaFile(e.Name))
				RaiseMediaFileRemoved(e.FullPath);
		}

		private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			if (IsMediaFile(e.Name))
				RaiseMediaFileRenamedEvent(e.FullPath, e.OldFullPath);
		}

		private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			if (IsMediaFile(e.Name))
				RaiseMediaFileFoundEvent(e.FullPath);
		}

		private void fileSystemWatcher_Error(object sender, ErrorEventArgs e)
		{
			var fileSystemWatcher = (FileSystemWatcher)sender;

			var path = fileSystemWatcher.Path;

			DisposeFileSystemWatcher(ref fileSystemWatcher);

			var mediaLocation =
				_mediaLocations.FirstOrDefault(pair => pair.Value.Path.Equals(path, StringComparison.CurrentCultureIgnoreCase));

			if (mediaLocation.Value != null && _mediaLocations.ContainsKey(mediaLocation.Key))
				AddMediaLocationWatcher(mediaLocation.Value);
		}

		#endregion File System Watchers

		public override void Dispose()
		{
			foreach (var watcher in _fileSystemWatchers.Value)
			{
				var fileSystemWatcher = watcher.Value;
				_fileSystemWatchers.Value.Remove(watcher.Key);

				DisposeFileSystemWatcher(ref fileSystemWatcher);
			}

			_fileExtensions = null;
		}

		private void DisposeFileSystemWatcher(ref FileSystemWatcher watcher)
		{
			watcher.Error -= fileSystemWatcher_Error;
			watcher.Created -= fileSystemWatcher_Created;
			watcher.Renamed -= fileSystemWatcher_Renamed;
			watcher.Deleted -= fileSystemWatcher_Deleted;

			watcher.Dispose();

			watcher = null;
		}
	}
}