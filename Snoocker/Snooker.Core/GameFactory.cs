using Snoocker.Core.Referees;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snoocker.Core
{
    public static class GameFactory
    {
        private static Dictionary<CueBallGameType, Func<ICueBallGameReferee, IPlayer, IPlayer, ICueBallGame>> _gameCreationDelegates;
        private static Dictionary<CueBallGameType, Func<IShotHistoryProvider, ICueBallGameReferee>> _gameReferee;
        private static Dictionary<CueBallGameType, Func<IShotHistoryProvider>> _shotHistoryProvider;

        public static TGameType CreatePoolGame<TGameType>(CueBallGameType cueBallGameType, ICueBallGameReferee referee,
            IPlayer player1, IPlayer player2)
        {
            return (TGameType)CreatePoolGame(cueBallGameType, referee, player1, player2);
        }

        public static ICueBallGameReferee CreateGameReferee(CueBallGameType gameType)
        {
            LazyInitializer.EnsureInitialized(ref _gameReferee, SetupGameRefereeCreationalDelegates);

            return _gameReferee[gameType](CreateShotHistoryProvider(gameType));
        }

        public static TGameType CreatePoolGame<TGameType>(CueBallGameType cueBallGameType,
            IPlayer player1, IPlayer player2)
        {
            return (TGameType)CreatePoolGame(cueBallGameType, CreateGameReferee(cueBallGameType), player1, player2);
        }

        private static ICueBallGame CreatePoolGame(CueBallGameType gameType, ICueBallGameReferee referee, IPlayer player1,
            IPlayer player2)
        {
            LazyInitializer.EnsureInitialized(ref _gameCreationDelegates, SetupGameCreationalDelegates);

            var game = _gameCreationDelegates[gameType](referee, player1, player2);

            return game;
        }

        private static IShotHistoryProvider CreateShotHistoryProvider(CueBallGameType gameType)
        {
            LazyInitializer.EnsureInitialized(ref _shotHistoryProvider, SetupGameStatusProviderStatus);

            return _shotHistoryProvider[gameType]();
        }

        private static Dictionary<CueBallGameType, Func<IShotHistoryProvider>> SetupGameStatusProviderStatus()
        {
            return new Dictionary<CueBallGameType, Func<IShotHistoryProvider>>()
            {
                {CueBallGameType.EightBall, () => null},
                {CueBallGameType.NineBall, () => null},
                {CueBallGameType.Blackball, () => null},
                {CueBallGameType.Snooker, () => new SnookerShotHistoryProvider()}
            };
        }

        private static Dictionary<CueBallGameType, Func<IShotHistoryProvider, ICueBallGameReferee>>
            SetupGameRefereeCreationalDelegates()
        {
            return new Dictionary<CueBallGameType, Func<IShotHistoryProvider, ICueBallGameReferee>>
            {
                {CueBallGameType.EightBall, provider => new EightBallGameReferee(provider)},
                {CueBallGameType.NineBall, provider => new NineBallGameReferee(provider)},
                {CueBallGameType.Blackball, provider => new BlackBallGameReferee(provider)},
                {CueBallGameType.Snooker, provider => new SnookerReferee(provider)}
            };
        }

        private static Dictionary<CueBallGameType, Func<ICueBallGameReferee, IPlayer, IPlayer, ICueBallGame>>
            SetupGameCreationalDelegates()
        {
            var gameCreationalDelegates = new Dictionary
                <CueBallGameType, Func<ICueBallGameReferee, IPlayer, IPlayer, ICueBallGame>>
            {
                {CueBallGameType.EightBall, (referee, player1, player2) => new EightBallGame(referee, player1, player2)},
                {CueBallGameType.NineBall, (referee, player1, player2) => new NineBallGame(referee, player1, player2)},
                {CueBallGameType.Blackball, (referee, player1, player2) => new BlackballGame(referee, player1, player2)},
                {CueBallGameType.Snooker, (referee, player1, player2) => new Snooker(referee, player1, player2)},
            };

            return gameCreationalDelegates;
        }
    }
}