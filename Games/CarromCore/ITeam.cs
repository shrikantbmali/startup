using Games.Common;

namespace CarromCore
{
	public interface ITeam : IIdentifiable
	{
		IPlayer PlayerA { get; }
		IPlayer PlayerB { get; }
	}
}