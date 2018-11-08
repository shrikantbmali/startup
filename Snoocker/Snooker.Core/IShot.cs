namespace Snoocker.Core
{
    public interface IShot
    {
        IPlayer Player { get; }
        Ball[] BallsHitOnRails { set; get; }
        Ball[] BallsPotted { set; get; }
        CueBallTouch FirstTouch { set; get; }
        Ball FirstTouchedBall { get; set; }
    }
}