using Snoocker.Core.Common;
using System.Linq;

namespace Snoocker.Core.Referees
{
    internal class EightBallGameReferee : CueBallGameReferee, ICueBallGameReferee
    {
        public EightBallGameReferee(IShotHistoryProvider provider) : base(provider)
        {
        }

        public override IGameResult IsValidBreak(IShot shot)
        {
            var gameResult = base.IsValidBreak(shot);

            if (gameResult.IsSuccessful)
            {
                if (shot.BallsHitOnRails.Length == 4)
                {
                    gameResult.Result = ShotResult.ValidShot;
                }

                if (shot.BallsPotted.Any(ball => ball.Is(BallTypes.Eight)))
                {
                    gameResult.Result |= ShotResult.CanReRack;
                }
            }

            return gameResult;
        }
    }
}