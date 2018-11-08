using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    public interface IShotHistoryProvider
    {
        void PushShotDetails(ShotDetails shotDetails);

        ShotDetails LastShotDetails { get; }
    }
}