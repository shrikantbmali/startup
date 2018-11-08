using Snoocker.Core.Common;
using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    internal class BlackballGame : CueBallGame, IBlackballGame
    {
        public BlackballGame(ICueBallGameReferee referee, IPlayer player1, IPlayer player2)
            : base(referee, CueBallGameType.Blackball, player1, player2)
        {
        }

        public override IGameResult Begine(IShot shot)
        {
            throw new System.NotImplementedException();
        }
    }
}