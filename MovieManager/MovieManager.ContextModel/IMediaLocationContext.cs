using MovieManager.StructureModel;

namespace MovieManager.ContextModel
{
	public interface IMediaLocationContext : IContext<MediaLocation, long>
	{
		bool IsLocationAlreadyAdded(string location);
	}
}