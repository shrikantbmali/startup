using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Snoocker.Core;
using Snoocker.Core.Referees;
using System.Collections.Generic;
using System.Linq;

namespace Snooker.Core.Tests
{
    [TestClass]
    public class Snoocker_On_Game_Initialization : BallGameTestbase
    {
        [TestMethod]
        public void Should_Have_All_Balls_On_Table()
        {
            var pool8BallGame = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker, CreateFirstPlayer(),
                CreateSecondPlayer());

            Assert.AreEqual(22, pool8BallGame.BallsOnTable.Count());
        }

        [TestMethod]
        public void Referee_Should_Validated_A_Valid_Breaking_Shot()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.Ball,
                    FirstTouchedBall = Ball.Red
                });

            Assert.IsTrue(breakResult.IsSuccessful);
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentContinues));
        }

        [TestMethod]
        public void Referee_Should_Validated_A_Invalid_Breaking_Shot_When_Other_Than_Red_Is_Potted()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.Ball,
                    FirstTouchedBall = Ball.Red,
                    BallsPotted = new[] { Ball.Red, Ball.Pink, }
                });

            Assert.IsTrue(breakResult.IsSuccessful);
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentContinues));
        }

        [TestMethod]
        public void Referee_Should_Validated_A_Valid_Breaking_Shot_When_Cue_Ball_FirstTouch_Is_Rail()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.Rail,
                    FirstTouchedBall = Ball.Red
                });

            Assert.IsTrue(breakResult.IsSuccessful);
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentContinues));
        }

        [TestMethod]
        public void Referee_Should_Validated_A_Valid_Breaking_Shot_When_Valid_Ball_Pots()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    BallsPotted = new[] { Ball.Red },
                    FirstTouch = CueBallTouch.Ball,
                    FirstTouchedBall = Ball.Red
                });

            Assert.IsTrue(breakResult.IsSuccessful);
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.PlayerContinues));
        }

        [TestMethod]
        public void Referee_Should_Validated_A_Valid_Breaking_Shot_When_Valid_Ball_Pots_First_Touch_Is_Rail()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    BallsPotted = new[] { Ball.Red },
                    FirstTouch = CueBallTouch.Rail,
                    FirstTouchedBall = Ball.Red
                });

            Assert.IsTrue(breakResult.IsSuccessful);
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.PlayerContinues));
        }

        [TestMethod]
        public void Referee_Should_Validated_An_Invalid_Breaking_Shot_When_Cue_Ball_Do_Not_Hit_Any_Ball()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.None,
                    FirstTouchedBall = Ball.None
                });

            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentCan));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.AskToPlayAgain));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.AcceptTable));
        }

        [TestMethod]
        public void Referee_Should_Validated_An_Invalid_Breaking_Shot_When_Cue_Ball_Do_Not_Hit_Any_Red()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.Ball,
                    FirstTouchedBall = Ball.Pink
                });

            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentCan));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.AskToPlayAgain));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.CanReRack));
        }

        [TestMethod]
        public void
            Referee_Should_Validated_An_Invalid_Breaking_Shot_When_Cue_Ball_Do_Not_Hit_Any_Red_While_First_Fouch_Is_Rail
            ()
        {
            var referee = GameFactory.CreateGameReferee(CueBallGameType.Snooker);

            referee.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());

            var breakResult =
                referee.IsValidBreak(new Shot(CreateFirstPlayer())
                {
                    FirstTouch = CueBallTouch.Rail,
                    FirstTouchedBall = Ball.Pink
                });

            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.OpenentCan));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.AskToPlayAgain));
            Assert.IsTrue(breakResult.Result.HasFlag(ShotResult.CanReRack));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Red()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.OpenentContinues));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.FreeBall));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Color()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.OpenentContinues));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Color_Pots_That_Color()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
                BallsPotted = new[] { Ball.Blue }
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.PlayerContinues));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Color_Pots_Other_Ball()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
                BallsPotted = new[] { Ball.Yellow }
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.OpenentContinues));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Color_Pots_More_Than_One_Ball()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
                BallsPotted = new[] { Ball.Blue, Ball.Black }
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.OpenentContinues));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.ReSpotColorBalls));
        }

        [TestMethod]
        public void Color_Followup_FirstPlayer_Hits_Color_Pots_NO_Ball()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreatePoolGame<ISnooker>(CueBallGameType.Snooker,
                firstPlayer, CreateSecondPlayer());
            snooker.CanCueBallSeeBothSideOfAnyRed += (sender, args) => args.CanCueBallSeeBothSideOfAnyRed = false;
            var gameStartResult = snooker.Begine(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });

            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.ValidShot));
            Assert.IsTrue(gameStartResult.Result.HasFlag(ShotResult.PlayerContinues));

            var shotResult = snooker.Shot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
            });

            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.OpenentContinues));
            Assert.IsTrue(shotResult.Result.HasFlag(ShotResult.ReSpotColorBalls));
        }

        [TestMethod]
        public void ColorRundown_FirstPlayer_Hits_Blue_When_Green_Was_On_Table()
        {
            var firstPlayer = CreateFirstPlayer();
            var snooker = GameFactory.CreateGameReferee(CueBallGameType.Snooker) as ISnookerReferee;
            var gameInsightProvider = new Mock<IGameInsightProvider>();
            gameInsightProvider.SetupGet(provider => provider.BallsOnTable).Returns(() => CueBallGameType.Snooker.GetAllGameBalls());
            snooker.Initialize(gameInsightProvider.Object);
            snooker.SetPlayers(CreateFirstPlayer(), CreateSecondPlayer());
            snooker.ValidateShot(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Red,
                BallsPotted = new[] { Ball.Red }
            });
            snooker.ValidateShot(new Shot(firstPlayer)
            {
                FirstTouch = CueBallTouch.Ball,
                FirstTouchedBall = Ball.Blue,
                BallsPotted = new[] {Ball.Blue}
            });
            gameInsightProvider.SetupGet(provider => provider.BallsOnTable).Returns(() => new List<Ball>
            {
                Ball.Cue,
                Ball.Black,
                Ball.Pink,
                Ball.Blue,
                Ball.Brown,
                Ball.Green,
            });
            var shotResult = snooker.ValidateShot(new Shot(firstPlayer)
            {
                FirstTouchedBall = Ball.Blue,
            });

            Assert.IsTrue(shotResult.HasFlag(ShotResult.InvalidShot));
            Assert.IsTrue(shotResult.HasFlag(ShotResult.OpenentContinues));
            Assert.IsTrue(shotResult.HasFlag(ShotResult.ReSpotColorBalls));
        }
    }
}