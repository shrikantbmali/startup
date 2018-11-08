using System;

namespace CarromCore
{
	public struct Player : IPlayer
	{
		public uint ID { get; private set; }

		public string Alias { get; private set; }

		public Player(uint id, string alias)
			: this()
		{
			if (string.IsNullOrEmpty(alias))
				throw new ArgumentException("Alias cannot be null or empty!");

			ID = id;
			Alias = alias;
		}
	}
}