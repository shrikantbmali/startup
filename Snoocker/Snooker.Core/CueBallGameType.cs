using System;

namespace Snoocker.Core
{
    [Flags]
    public enum CueBallGameType : byte
    {
        None = 0,
        EightBall = 1,
        NineBall = 2,
        Blackball = 4,
        Snooker = 8,
        All = EightBall | NineBall | Blackball | Snooker,
    }
}