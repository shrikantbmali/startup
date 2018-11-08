namespace Snoocker.Core
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public BallGroupTypes GameBallGroup { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public bool Equals(IPlayer other)
        {
            return Name.Equals(other.Name) &&
                   GameBallGroup == other.GameBallGroup;
        }

        public bool Is(IPlayer other)
        {
            return Equals(other);
        }
    }
}