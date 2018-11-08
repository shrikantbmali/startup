using Snoocker.Core.Common;

namespace Snoocker.Core.Referees
{
    public interface ICueBallGameReferee
    {
        void SetPlayers(IPlayer player1, IPlayer player2);

        IGameResult IsValidBreak(IShot shot);

        ShotResult ValidateShot(IShot shot);

        void Initialize(IGameInsightProvider insightProvider);
    }
}