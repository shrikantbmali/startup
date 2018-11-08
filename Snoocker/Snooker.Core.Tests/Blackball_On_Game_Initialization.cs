using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snoocker.Core;
using System.Linq;

namespace Snooker.Core.Tests
{
    [TestClass]
    public class Blackball_On_Game_Initialization : BallGameTestbase
    {
        [TestMethod]
        public void Should_Have_All_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<IBlackballGame>(CueBallGameType.Blackball,
                CreateFirstPlayer(), CreateSecondPlayer());

            Assert.AreEqual(16, pool8BallGame.BallsOnTable.Count());
        }

        [TestMethod]
        public void Should_Have_All_Correct_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<IBlackballGame>(CueBallGameType.Blackball,
                CreateFirstPlayer(), CreateSecondPlayer());

            var enumerable =
                pool8BallGame.BallsOnTable.Where(ball => ball.BallType == BallTypes.Yellow).Select(ball => ball);

            Assert.AreEqual(7, enumerable.Count());

            enumerable = pool8BallGame.BallsOnTable.Where(ball => ball.BallType == BallTypes.Red).Select(ball => ball);

            Assert.AreEqual(7, enumerable.Count());

            enumerable = pool8BallGame.BallsOnTable.Where(ball => ball.BallType == BallTypes.Cue).Select(ball => ball);

            Assert.AreEqual(1, enumerable.Count());

            enumerable = pool8BallGame.BallsOnTable.Where(ball => ball.BallType == BallTypes.Black).Select(ball => ball);

            Assert.AreEqual(1, enumerable.Count());
        }
    }
}