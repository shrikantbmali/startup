using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snoocker.Core;
using System.Collections.Generic;
using System.Linq;

namespace Snooker.Core.Tests
{
    [TestClass]
    public class NineBall_On_Game_Initialization : BallGameTestbase
    {
        [TestMethod]
        public void Should_Be_A_Open_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<INineBallGame>(CueBallGameType.NineBall, CreateFirstPlayer(), CreateSecondPlayer());

            Assert.AreEqual(GamePhases.OpenTable, pool8BallGame.GamePhase);
        }

        [TestMethod]
        public void Should_Have_All_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<INineBallGame>(CueBallGameType.NineBall, CreateFirstPlayer(),
                CreateSecondPlayer());

            Assert.AreEqual(10, pool8BallGame.BallsOnTable.Count());
        }

        [TestMethod]
        public void Should_Have_All_Correct_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<INineBallGame>(CueBallGameType.NineBall, CreateFirstPlayer(), CreateSecondPlayer());

            var ballsOnTable = pool8BallGame.BallsOnTable;

            var ballTypes = new List<BallTypes>
            {
                BallTypes.Cue,

                BallTypes.One,
                BallTypes.Two,
                BallTypes.Three,
                BallTypes.Four,
                BallTypes.Five,
                BallTypes.Six,
                BallTypes.Seven,
                BallTypes.Eight,
                BallTypes.Nine,
            };

            var ballAlreadyOnBoard = new List<Ball>();

            foreach (var gameBall in ballsOnTable)
            {
                if (ballAlreadyOnBoard.Contains(gameBall))
                {
                    Assert.Fail("Repeate ball");
                }

                ballAlreadyOnBoard.Add(gameBall);
                Assert.IsTrue(ballTypes.Contains(gameBall.BallType), "Ball type : " + gameBall.BallType + " is incorrect");
            }
        }
    }
}