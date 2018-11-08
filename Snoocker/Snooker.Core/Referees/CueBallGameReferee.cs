using Snoocker.Core.Common;
using System;

namespace Snoocker.Core.Referees
{
    internal abstract class CueBallGameReferee : ICueBallGameReferee
    {
        protected IGameInsightProvider GameInsightProvider { get; private set; }
        protected IPlayer Player1 { get; private set; }
        protected IPlayer Player2 { get; private set; }
        protected IShotHistoryProvider ShotHistoryProvider { get; private set; }

        public CueBallGameReferee(IShotHistoryProvider shotHistoryProvider)
        {
            ShotHistoryProvider = shotHistoryProvider;
        }

        public void SetPlayers(IPlayer player1, IPlayer player2)
        {
            Player2 = player2;
            Player1 = player1;
        }

        public virtual IGameResult IsValidBreak(IShot shot)
        {
            var gameResult = new GameResult();

            if (!ArePlayerSet())
                throw new InvalidOperationException("Players are not set yet!");

            gameResult.IsSuccessful = true;

            return gameResult;
        }

        public virtual ShotResult ValidateShot(IShot shot)
        {
            throw new NotImplementedException();
        }

        public void Initialize(IGameInsightProvider insightProvider)
        {
            GameInsightProvider = insightProvider;
        }

        private bool ArePlayerSet()
        {
            return Player1 != null && Player2 != null;
        }
    }
}