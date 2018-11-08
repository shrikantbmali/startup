using Snoocker.Core.Common;
using Snoocker.Core.Referees;
using System;

namespace Snoocker.Core
{
    internal class NineBallGame : CueBallGame, INineBallGame
    {
        public NineBallGame(ICueBallGameReferee referee, IPlayer player1, IPlayer player2)
            : base(referee, CueBallGameType.NineBall, player1, player2)
        {
            GamePhase = GamePhases.OpenTable;
        }

        public override IGameResult Begine(IShot shot)
        {
            throw new NotImplementedException();
        }
    }
}