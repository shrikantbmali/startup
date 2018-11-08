namespace CarromCore
{
	public interface ICarrom
	{
		int WhiteGoti { get; }

		int BlackGoti { get; }

		bool IsQueenAlive { get; }

		IMoveResult Move(IMove move);
	}
}