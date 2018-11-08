using Games.Common;

namespace CarromCore
{
	public interface IPlayer : IIdentifiable
	{
		string Alias { get; }
	}
}