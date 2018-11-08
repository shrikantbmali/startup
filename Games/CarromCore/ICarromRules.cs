namespace CarromCore
{
	public interface ICarromRules : IGameRule
	{
		bool IsMoveValid(IMove move);
	}
}