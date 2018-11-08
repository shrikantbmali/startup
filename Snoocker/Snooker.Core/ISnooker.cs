using System;

namespace Snoocker.Core
{
    public interface ISnooker : ICueBallGame
    {
        event EventHandler<CueBallPerspectiveEventArgs> CanCueBallSeeBothSideOfAnyRed;
    }
}