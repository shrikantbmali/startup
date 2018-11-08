namespace Snoocker.Core.Referees
{
    public struct ShotDetails
    {
        public IPlayer Player;
        public ShotResult ShotResult;
        public Ball LastPlayedBall;
        public IShot Shot;
    }
}