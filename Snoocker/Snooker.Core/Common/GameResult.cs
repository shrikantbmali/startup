using System.Collections.Generic;

namespace Snoocker.Core.Common
{
    internal struct GameResult : IGameResult
    {
        public bool IsSuccessful { get; set; }
        public ShotResult Result { get; set; }
        public IEnumerable<Ball> BallsToRespot { get; }

        public GameResult(bool isSuccessful, ShotResult result)
            : this()
        {
            IsSuccessful = isSuccessful;
            Result = result;
        }
    }
}