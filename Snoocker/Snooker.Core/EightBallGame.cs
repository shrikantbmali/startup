using Snoocker.Core.Common;
using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    internal class EightBallGame : CueBallGame, IEightBallGame
    {
        public uint PottedBalls { get; private set; }

        public EightBallGame(ICueBallGameReferee referee, IPlayer player1, IPlayer player2)
            : base(referee, CueBallGameType.EightBall, player1, player2)
        {
            GamePhase = GamePhases.OpenTable;
        }

        public override IGameResult Begine(IShot shot)
        {
            var gameResult = base.Begine(shot);

            return gameResult;
        }
    }
}