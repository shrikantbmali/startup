using System.Collections.Generic;

namespace Snoocker.Core.Common
{
    public interface IGameResult
    {
        bool IsSuccessful { get; set; }

        ShotResult Result { get; set; }

        IEnumerable<Ball> BallsToRespot { get; }
    }
}