namespace CarromCore
{
	internal struct Move : IMove
	{
		public IPlayer Player { get; set; }

		public ITeam Team { get; set; }

		public byte WhiteGotiPotted { get; private set; }

		public byte BlackGotiPotted { get; private set; }

		public bool IsQueenPotted { get; private set; }
	}
}