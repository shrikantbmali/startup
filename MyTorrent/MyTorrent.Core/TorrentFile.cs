using System.IO;

namespace MyTorrent.Core
{
	public class TorrentFile : ITorrentFile
	{
		private readonly string _filePath;

		public TorrentFile(string filePath)
		{
			_filePath = filePath;
		}

		public TorrentFile(StreamReader reader)
		{
			
		}

		public void Open()
		{
			using (var fileStream = File.Open(_filePath, FileMode.Open))
			{
				using (var streamReader = new StreamReader(fileStream))
				{
					var readToEnd = streamReader.ReadToEnd();
				}
			}
			
		}
	}
}