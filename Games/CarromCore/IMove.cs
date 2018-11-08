namespace CarromCore
{
	public interface IMove
	{
		IPlayer Player { get; set; }

		ITeam Team { get; set; }

		byte WhiteGotiPotted { get; }

		byte BlackGotiPotted { get; }

		bool IsQueenPotted { get; }
	}
}