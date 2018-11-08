using System;

namespace Snoocker.Core
{
    public enum BallTypes
    {
        Cue,

        Red,

        Yellow,
        Brown,
        Green,
        Blue,
        Pink,
        Black,

        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,

        Eight,

        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        None
    }

    [Flags]
    public enum BallGroupTypes : byte
    {
        None = 0,

        Cue = 1,

        Reds = 2,

        Colors = 4,

        Solids = 8,

        Eight = 16,

        Strips = 32
    }
}