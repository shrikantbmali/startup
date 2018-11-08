namespace CarromCore
{
	internal class Match : IMatch
	{
		public ITeam TeamA { get; private set; }

		public ITeam TeamB { get; private set; }

		public Match(ITeam teamA, ITeam teamB)
		{
			TeamA = teamA;
			TeamB = teamB;
		}
	}
}