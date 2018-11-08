using Snoocker.Core.Common;
using System.Linq;

namespace Snoocker.Core.Referees
{
    internal class SnookerReferee : CueBallGameReferee, ISnookerReferee
    {
        public SnookerReferee(IShotHistoryProvider shotHistoryProvider) : base(shotHistoryProvider)
        {
        }

        public override IGameResult IsValidBreak(IShot shot)
        {
            var breakResult = base.IsValidBreak(shot);

            if (breakResult.IsSuccessful)
            {
                breakResult.Result = ValidateShot(shot);

                ShotHistoryProvider.PushShotDetails(CreateShotHistory(shot, breakResult));
            }

            return breakResult;
        }

        public override ShotResult ValidateShot(IShot shot)
        {
            // 1. He can hit a red. (Both player)
            // Scenarios
            //  a. Fresh Hit (New Player)
            //  b. Color Followup (Old Player)
            // 2. He can hit a color (Both Players)
            // Scenarios
            //  a. Red followup (Old Player)
            //  b. Free Ball (New Player) **** // TODO
            //  c. ColorRundown (Both Players)
            var shotResult = ShotResult.Undetermined;

            if (ShouldHitColor(shot))
            {
                shotResult = ValidateColorHit(shot);
            }
            else if (shot.FirstTouchedBall.Is(BallTypes.Red))
            {
                shotResult = ShotResult.ValidShot;

                if (shot.BallsPotted.Length > 0 && shot.BallsPotted.All(ball => ball.Is(BallTypes.Red)))
                {
                    shotResult |= ShotResult.PlayerContinues;
                }
                else if (shot.BallsPotted.Length > 0 && shot.BallsPotted.Any(ball => !ball.Is(BallTypes.Red)))
                {
                    shotResult |= ShotResult.OpenentContinues;
                    shotResult |= ShotResult.InvalidShot;
                }
                else if (shot.BallsPotted.Length == 0)
                {
                    shotResult |= ShotResult.OpenentContinues;
                }
            }
            else if (shot.FirstTouchedBall.Is(BallTypes.None))
            {
                shotResult = ShotResult.InvalidShot | ShotResult.OpenentCan | ShotResult.AskToPlayAgain |
                                     ShotResult.AcceptTable;
            }
            else if (!shot.FirstTouchedBall.Is(BallTypes.Red))
            {
                shotResult = ShotResult.InvalidShot | ShotResult.OpenentCan | ShotResult.AskToPlayAgain |
                                     ShotResult.CanReRack;
            }

            ShotHistoryProvider.PushShotDetails(CreateShotHistory(shot, new GameResult()
            {
                IsSuccessful = true,
                Result = shotResult
            }));
            return shotResult;
        }

        private ShotResult ValidateColorHit(IShot shot)
        {
            var shotResult = ShotResult.Undetermined;

            if (shot.FirstTouchedBall.IsBallInGroup(BallGroupTypes.Colors))
            {
                if (shot.BallsPotted.Length == 0)
                {
                    shotResult = ShotResult.ValidShot | ShotResult.OpenentContinues;
                }
                else if (shot.BallsPotted.Length == 1 &&
                         shot.BallsPotted.All(ball => ball.IsBallInGroup(BallGroupTypes.Colors)))
                {
                    if (shot.BallsPotted.Any(ball => !ball.Is(shot.FirstTouchedBall.BallType)))
                    {
                        shotResult = ShotResult.InvalidShot | ShotResult.OpenentContinues;
                    }
                    else
                    {
                        shotResult = ShotResult.ValidShot | ShotResult.PlayerContinues;
                    }
                }
                else if (shot.BallsPotted.Length > 1)
                {
                    shotResult = ShotResult.InvalidShot | ShotResult.OpenentContinues | ShotResult.ReSpotColorBalls;
                }
            }
            //This check will cover the scenarion when cue ball did not hit any other ball.
            else if (!shot.FirstTouchedBall.IsBallInGroup(BallGroupTypes.Colors))
            {
                shotResult = ShotResult.InvalidShot | ShotResult.OpenentContinues;

                if (!GameInsightProvider.CanCueBallSeeBothSidesOfAnyRed())
                {
                    shotResult |= ShotResult.FreeBall;
                }
            }

            return shotResult;
        }

        private bool ShouldHitColor(IShot shot)
        {
            return WasLastPottedBallRed(shot) || IsColorRunDownInProgress();
        }

        private bool IsColorRunDownInProgress()
        {
            var anyRedOnTable = GameInsightProvider.BallsOnTable.Any(ball => ball.Is(BallTypes.Red));

            return !anyRedOnTable;
        }

        private bool WasLastPottedBallRed(IShot shot)
        {
            return ShotHistoryProvider.LastShotDetails.ShotResult.HasFlag(ShotResult.ValidShot)
                   && ShotHistoryProvider.LastShotDetails.ShotResult.HasFlag(ShotResult.PlayerContinues)
                   && ShotHistoryProvider.LastShotDetails.Player.Equals(shot.Player)
                   && ShotHistoryProvider.LastShotDetails.Shot.BallsPotted.All(ball => ball.Is(BallTypes.Red));
        }

        private void ValidateFreeBall(IShot shot)
        {
            throw new System.NotImplementedException();
        }

        private static ShotDetails CreateShotHistory(IShot shot, IGameResult breakResult)
        {
            return new ShotDetails
            {
                ShotResult = breakResult.Result,
                LastPlayedBall = shot.FirstTouchedBall,
                Player = shot.Player,
                Shot = shot
            };
        }
    }
}