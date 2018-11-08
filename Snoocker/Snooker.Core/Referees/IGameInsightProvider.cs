using System.Collections.Generic;

namespace Snoocker.Core.Referees
{
    public interface IGameInsightProvider
    {
        IEnumerable<Ball> BallsOnTable { get; }

        GamePhases GamePhase { get; }

        bool CanCueBallSeeBothSidesOfAnyRed();
    }
}