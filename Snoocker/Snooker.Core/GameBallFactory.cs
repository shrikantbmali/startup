using System;
using System.Collections.Generic;
using System.Linq;

namespace Snoocker.Core
{
    public static class GameBallFactory
    {
        private static readonly Dictionary<CueBallGameType, Func<IEnumerable<Ball>>> _gameBallCreators;

        static GameBallFactory()
        {
            _gameBallCreators = new Dictionary<CueBallGameType, Func<IEnumerable<Ball>>>()
            {
                {CueBallGameType.EightBall, () => GetGameBallOfType(CueBallGameType.EightBall)},
                {CueBallGameType.NineBall, () => GetGameBallOfType(CueBallGameType.NineBall)},
                {CueBallGameType.Blackball, CreateGameBallsForBlackballGame},
                {CueBallGameType.Snooker, CreateGameBallsForSnooker},
            };
        }

        private static IEnumerable<Ball> GetAllBalls()
        {
            var gameBalls = new List<Ball>
            {
                Ball.Cue,

                Ball.Red,

                Ball.Yellow,
                Ball.Brown,
                Ball.Green,
                Ball.Blue,
                Ball.Pink,
                Ball.Black,

                Ball.One,
                Ball.Two,
                Ball.Three,
                Ball.Four,
                Ball.Five,
                Ball.Six,
                Ball.Seven,
                Ball.Eight,
                Ball.Nine,
                Ball.Ten,
                Ball.Eleven,
                Ball.Twelve,
                Ball.Thirteen,
                Ball.Fourteen,
                Ball.Fifteen
            };

            return gameBalls;
        }

        public static IEnumerable<Ball> GetAllGameBalls(this CueBallGameType cueBallGameType)
        {
            return _gameBallCreators[cueBallGameType]();
        }

        private static IEnumerable<Ball> GetGameBallOfType(CueBallGameType cueBallGameType)
        {
            return GetAllBalls().Where(gameBall => gameBall.IsBallOfGameType(cueBallGameType));
        }

        private static IEnumerable<Ball> CreateGameBallsForBlackballGame()
        {
            var gameBalls = new List<Ball> { Ball.Cue, Ball.Black };

            for (var i = 0; i < 7; i++)
            {
                gameBalls.Add(Ball.Yellow);
            }
            for (var i = 0; i < 7; i++)
            {
                gameBalls.Add(Ball.Red);
            }

            return gameBalls;
        }

        private static IEnumerable<Ball> CreateGameBallsForSnooker()
        {
            var gameBalls = GetGameBallOfType(CueBallGameType.Snooker).ToList();
            var gameBall = gameBalls.First(ball => ball.BallType == BallTypes.Red);
            gameBalls.Remove(gameBall);

            for (var i = 0; i < 15; i++)
            {
                gameBalls.Add(Ball.Red);
            }

            return gameBalls;
        }
    }
}