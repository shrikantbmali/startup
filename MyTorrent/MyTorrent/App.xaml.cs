using MyTorrent.Core;
using System.Windows;

namespace MyTorrent
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var torrentFile = new TorrentFile(@"C:\Users\Sam\Downloads\Forever+US+S01E02+HDTV+x264-LOL+%5Beztv%5D.torrent");

			torrentFile.Open();
		}
	}
}