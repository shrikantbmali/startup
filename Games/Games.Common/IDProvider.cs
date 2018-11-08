using System;

namespace Games.Common
{
	public class IDProvider : IIDProvider
	{
		private static uint _currentID = 0;
		private static readonly Lazy<IIDProvider> _lazyInstance = new Lazy<IIDProvider>(() => new IDProvider());

		public static IIDProvider Instance
		{
			get { return _lazyInstance.Value; }
		}

		public uint GenerateID()
		{
			return _currentID++;
		}
	}
}