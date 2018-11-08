using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snoocker.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snooker.Core.Tests
{
    [TestClass]
    public class EightBallGame_On_Game_Initialization : BallGameTestbase
    {
        [TestMethod]
        public void Should_Be_A_Open_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<IEightBallGame>(CueBallGameType.EightBall,
                CreateFirstPlayer(), CreateSecondPlayer());

            Assert.AreEqual(GamePhases.OpenTable, pool8BallGame.GamePhase);
        }

        [TestMethod]
        public void Should_Have_All_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<IEightBallGame>(CueBallGameType.EightBall,
                CreateFirstPlayer(), CreateSecondPlayer());

            Assert.AreEqual(16, pool8BallGame.BallsOnTable.Count());
        }

        [TestMethod]
        public void Should_Have_All_Correct_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<IEightBallGame>(CueBallGameType.EightBall,
                CreateFirstPlayer(), CreateSecondPlayer());

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
                BallTypes.Ten,
                BallTypes.Eleven,
                BallTypes.Twelve,
                BallTypes.Thirteen,
                BallTypes.Fourteen,
                BallTypes.Fifteen
            };

            var ballAlreadyOnBoard = new List<Ball>();

            foreach (var gameBall in ballsOnTable)
            {
                if (ballAlreadyOnBoard.Contains(gameBall))
                {
                    Assert.Fail("Repeate ball");
                }
                {
                    Assert.IsTrue(ballTypes.Contains(gameBall.BallType),
                        "Ball type : " + gameBall.BallType + " is incorrect");
                }
            }
        }

        [TestMethod]
        public void Balls_Should_Be_Equatible()
        {
            Assert.IsTrue(Ball.Cue.Equals(Ball.Cue));
            Assert.IsFalse(Ball.Cue.Equals(Ball.Eight));

            var gameBall = Ball.Cue;
            Assert.IsTrue(gameBall.Is(Ball.Cue));
        }

        [TestMethod]
        public void Invalid_Player_Cannot_Play_A_Shot()
        {
            var eightBallGame = GameFactory.CreatePoolGame<IEightBallGame>(CueBallGameType.EightBall,
                CreateFirstPlayer(), CreateSecondPlayer());

            var gameResult = eightBallGame.Shot(new Shot(new Player("Invalid_Player")));

            Assert.IsFalse(gameResult.IsSuccessful);
            Assert.AreEqual(ShotResult.InvalidPlayer, gameResult.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void First_Shot_Before_Player_Set_Throws_Exception()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.EightBall);

            var shot = new Shot(new Player("Valid_Player"));

            referee.IsValidBreak(shot);
        }

        [TestMethod]
        public void Breaking_Shot_Should_Make_Four_Ball_To_Hit_The_Rails()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.EightBall);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var shot = new Shot(CreateFirstPlayer());

            shot.BallsHitOnRails = new[]
            {
                Ball.One,
                Ball.Black,
                Ball.Five,
                Ball.Fifteen
            };

            var gameResult = referee.IsValidBreak(shot);

            Assert.IsTrue(gameResult.IsSuccessful);
            Assert.AreEqual(gameResult.Result, ShotResult.ValidShot);
        }

        [TestMethod]
        public void Referee_Break_EightBall_Pots()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.EightBall);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var shot = new Shot(CreateFirstPlayer()) as IShot;

            shot.BallsHitOnRails = new[]
            {
                Ball.One,
                Ball.Black,
                Ball.Five,
                Ball.Fifteen
            };

            shot.BallsPotted = new[]
            {
                Ball.One, Ball.Twelve, Ball.Eight
            };

            var gameResult = referee.IsValidBreak(shot);

            Assert.IsTrue(gameResult.IsSuccessful);
            Assert.IsTrue(gameResult.Result.HasFlag(ShotResult.ValidShot), "Should be a valid shot.");
            Assert.IsTrue(gameResult.Result.HasFlag(ShotResult.CanReRack), "Should have choice of Re-Rack");
        }
    }
}