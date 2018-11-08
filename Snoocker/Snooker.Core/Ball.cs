using Snoocker.Core.Common;
using System;

namespace Snoocker.Core
{
    public class Ball : IEquatable<Ball>, IIsComparable<Ball>, IIsComparable<BallTypes>
    {
        public BallTypes BallType { get; }
        public BallGroupTypes BallGroupType { get; }
        public CueBallGameType CueBallGameType { get; }
        public byte Weight { get; }

        #region Balls

        public static Ball Cue => new Ball(BallTypes.Cue, CueBallGameType.All, 0, BallGroupTypes.Cue);

        public static Ball Red => new Ball(BallTypes.Red, CueBallGameType.Blackball | CueBallGameType.Snooker, 1, BallGroupTypes.Reds);

        public static Ball Yellow => new Ball(BallTypes.Yellow, CueBallGameType.Blackball | CueBallGameType.Snooker, 2, BallGroupTypes.Colors);

        public static Ball Brown => new Ball(BallTypes.Brown, CueBallGameType.Snooker, 3, BallGroupTypes.Colors);

        public static Ball Green => new Ball(BallTypes.Green, CueBallGameType.Snooker, 4, BallGroupTypes.Colors);

        public static Ball Blue => new Ball(BallTypes.Blue, CueBallGameType.Snooker, 5, BallGroupTypes.Colors);

        public static Ball Pink => new Ball(BallTypes.Pink, CueBallGameType.Snooker, 6, BallGroupTypes.Colors);

        public static Ball Black => new Ball(BallTypes.Black, CueBallGameType.Blackball | CueBallGameType.Snooker, 7, BallGroupTypes.Colors);

        public static Ball One => new Ball(BallTypes.One, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Two => new Ball(BallTypes.Two, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Three => new Ball(BallTypes.Three, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Four => new Ball(BallTypes.Four, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Five => new Ball(BallTypes.Five, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Six => new Ball(BallTypes.Six, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Seven => new Ball(BallTypes.Seven, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Solids);

        public static Ball Eight => new Ball(BallTypes.Eight, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Eight);

        public static Ball Nine => new Ball(BallTypes.Nine, CueBallGameType.EightBall | CueBallGameType.NineBall, 0, BallGroupTypes.Strips);

        public static Ball Ten => new Ball(BallTypes.Ten, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball Eleven => new Ball(BallTypes.Eleven, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball Twelve => new Ball(BallTypes.Twelve, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball Thirteen => new Ball(BallTypes.Thirteen, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball Fourteen => new Ball(BallTypes.Fourteen, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball Fifteen => new Ball(BallTypes.Fifteen, CueBallGameType.EightBall, 0, BallGroupTypes.Strips);

        public static Ball None => new Ball(BallTypes.None, Core.CueBallGameType.None, 0, BallGroupTypes.None);

        #endregion Balls

        public Ball(BallTypes ballType)
        {
            BallType = ballType;
        }

        public Ball(BallTypes ballType, CueBallGameType cueBallGameType, byte weight)
            : this(ballType)
        {
            CueBallGameType = cueBallGameType;
            Weight = weight;
        }

        public Ball(BallTypes ballType, CueBallGameType cueBallGameType, byte weight, BallGroupTypes ballGroupType)
            : this(ballType, cueBallGameType, weight)
        {
            BallGroupType = ballGroupType;
        }

        public bool IsBallOfGameType(CueBallGameType cueBallGameType)
        {
            return CueBallGameType.HasFlag(cueBallGameType);
        }

        public bool IsBallInGroup(BallGroupTypes ballGroupType)
        {
            return BallGroupType.HasFlag(ballGroupType);
        }

        public bool Equals(Ball other)
        {
            return this.CueBallGameType == other.CueBallGameType
                   && this.BallGroupType == other.BallGroupType
                   && this.Weight == other.Weight
                   && this.BallType == other.BallType;
        }

        public bool Is(Ball other)
        {
            return this.Equals(other);
        }

        public bool Is(BallTypes other)
        {
            return this.BallType == other;
        }
    }
}