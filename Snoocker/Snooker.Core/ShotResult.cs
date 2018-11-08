using System;

namespace Snoocker.Core
{
    [Flags]
    public enum ShotResult : uint
    {
        // This state needs have 0 value,  so it remains as default in all the cases without mention.
        Undetermined = 0,

        ValidShot = 1,
        PlayerContinues = 2,

        InvalidPlayer = 4,

        CanReRack = 8,
        AcceptTable = 16,

        AskToPlayAgain = 32,

        OpenentCan = 64,

        InvalidShot = 128,
        OpenentContinues = 264,

        ReSpotColorBalls = 512,

        FreeBall = 1024
    }
}