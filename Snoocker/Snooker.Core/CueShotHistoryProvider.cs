using System.Collections.Generic;
using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    internal abstract class CueShotHistoryProvider : IShotHistoryProvider
    {
        private int _index;
        private readonly Dictionary<int, ShotDetails> _statuses = new Dictionary<int, ShotDetails>();

        public ShotDetails LastShotDetails
        {
            get
            {
                var i = _index - 1;
                if (i == -1)
                {
                    return new ShotDetails()
                    {
                        LastPlayedBall = Ball.None,
                        ShotResult = ShotResult.Undetermined,
                        Player = null,
                    };
                }

                return _statuses[i];
            }
        }

        public void PushShotDetails(ShotDetails shotDetails)
        {
            _statuses.Add(_index++, shotDetails);
        }
    }
}