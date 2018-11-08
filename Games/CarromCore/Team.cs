using System;

namespace CarromCore
{
	public struct Team : ITeam
	{
		public IPlayer PlayerA { get; private set; }
		public IPlayer PlayerB { get; private set; }

		public uint ID { get; private set; }

		public Team(uint id, IPlayer playerA, IPlayer playerB)
			: this()
		{
			if (playerA.ID == playerB.ID)
				throw new ArgumentException("Teams with same ID cannot be added!");

			ID = id;
			PlayerA = playerA;
			PlayerB = playerB;
		}
	}
}