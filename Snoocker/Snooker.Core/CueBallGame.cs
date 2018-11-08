using Snoocker.Core.Common;
using Snoocker.Core.Referees;
using System.Collections.Generic;

namespace Snoocker.Core
{
    internal abstract class CueBallGame : ICueBallGame
    {
        private readonly ICueBallGameReferee _cueBallGameReferee;
        private readonly List<Ball> _ballsOnTable;

        public IEnumerable<Ball> BallsOnTable => _ballsOnTable;

        public GamePhases GamePhase { get; protected set; }

        public virtual bool CanCueBallSeeBothSidesOfAnyRed()
        {
            return true;
        }

        protected CueBallGame(ICueBallGameReferee cueBallGameReferee, CueBallGameType gameType, IPlayer player1, IPlayer player2)
        {
            _cueBallGameReferee = cueBallGameReferee;
            _ballsOnTable = new List<Ball>();

            foreach (var ballType in gameType.GetAllGameBalls())
            {
                AddBall(ballType);
            }

            _cueBallGameReferee.Initialize(this);
            _cueBallGameReferee.SetPlayers(player1, player2);
        }

        public virtual IGameResult Begine(IShot shot)
        {
            var shotResult = _cueBallGameReferee.IsValidBreak(shot);

            return shotResult;
        }

        public IGameResult End()
        {
            throw new System.NotImplementedException();
        }

        public IGameResult Shot(IShot shot)
        {
            var shotResult = _cueBallGameReferee.ValidateShot(shot);

            return new GameResult(true, shotResult);
        }

        private void AddBall(Ball ball)
        {
            _ballsOnTable.Add(ball);
        }
    }
}