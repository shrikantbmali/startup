using Snoocker.Core.Common;

namespace Snoocker.Core.Referees
{
    internal class BlackBallGameReferee : CueBallGameReferee, ICueBallGameReferee
    {
        public BlackBallGameReferee(IShotHistoryProvider shotHistoryProvider) : base(shotHistoryProvider)
        {
        }

        public override IGameResult IsValidBreak(IShot shot)
        {
            throw new System.NotImplementedException();
        }
    }
}