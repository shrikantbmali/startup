using Snoocker.Core;

namespace Snooker.Core.Tests
{
    public class BallGameTestbase
    {
        protected static Player CreateSecondPlayer()
        {
            return new Player("second");
        }

        protected static Player CreateFirstPlayer()
        {
            return new Player("first");
        }
    }
}