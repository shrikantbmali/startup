using Snoocker.Core.Common;
using System;

namespace Snoocker.Core
{
    public interface IPlayer : IEquatable<IPlayer>, IIsComparable<IPlayer>
    {
        string Name { get; set; }
        BallGroupTypes GameBallGroup { get; set; }
    }
}