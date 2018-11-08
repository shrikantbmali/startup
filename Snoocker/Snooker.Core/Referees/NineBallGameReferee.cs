using Snoocker.Core.Common;

namespace Snoocker.Core.Referees
{
    internal class NineBallGameReferee : CueBallGameReferee, ICueBallGameReferee
    {
        public NineBallGameReferee(IShotHistoryProvider shotHistoryProvider) : base(shotHistoryProvider)
        {
        }

        public override IGameResult IsValidBreak(IShot shot)
        {
            throw new System.NotImplementedException();
        }
    }
}