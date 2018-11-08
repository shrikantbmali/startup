using Snoocker.Core.Common;
using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    public interface ICueBallGame : IGameInsightProvider
    {
        IGameResult Begine(IShot shot);

        IGameResult End();

        IGameResult Shot(IShot shot);
    }
}