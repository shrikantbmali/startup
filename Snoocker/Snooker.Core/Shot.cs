namespace Snoocker.Core
{
    public struct Shot : IShot
    {
        private static readonly Ball[] EmptyBallArray = { };

        public Ball[] BallsHitOnRails { get; set; }

        public Ball[] BallsPotted { get; set; }

        public CueBallTouch FirstTouch { get; set; }

        public Ball FirstTouchedBall { get; set; }

        public IPlayer Player { get; }

        public Shot(IPlayer player) : this()
        {
            Player = player;

            BallsHitOnRails = BallsPotted = EmptyBallArray;

            FirstTouchedBall = Ball.None;
        }
    }
}