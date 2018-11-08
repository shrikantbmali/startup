using System;
using Snoocker.Core.Referees;

namespace Snoocker.Core
{
    internal class Snooker : CueBallGame, ISnooker
    {
        public event EventHandler<CueBallPerspectiveEventArgs> CanCueBallSeeBothSideOfAnyRed;

        public Snooker(ICueBallGameReferee cueBallGameReferee, IPlayer player1, IPlayer player2)
            : base(cueBallGameReferee, CueBallGameType.Snooker, player1, player2)
        {
        }

        public override bool CanCueBallSeeBothSidesOfAnyRed()
        {
            var cueBallPerspectiveEventArgs = new CueBallPerspectiveEventArgs();

            var handler = CanCueBallSeeBothSideOfAnyRed;
            handler?.Invoke(this, cueBallPerspectiveEventArgs);

            return cueBallPerspectiveEventArgs.CanCueBallSeeBothSideOfAnyRed;
        }
    }
}