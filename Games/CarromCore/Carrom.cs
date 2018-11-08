namespace CarromCore
{
	public class Carrom : ICarrom
	{
		private readonly ICarromRules _rules;
		private readonly IMatch _match;

		public int WhiteGoti { get; private set; }

		public int BlackGoti { get; private set; }

		public bool IsQueenAlive { get; private set; }

		public Carrom(ICarromRules rules, IMatch match)
		{
			_rules = rules;
			_match = match;

			Initialize();
		}

		public IMoveResult Move(IMove move)
		{
			if (_rules.IsMoveValid(move))
			{
				WhiteGoti -= move.WhiteGotiPotted;
				BlackGoti -= move.BlackGotiPotted;
				IsQueenAlive = move.IsQueenPotted;
			}

			return new MoveResult();
		}

		private void Initialize()
		{
			WhiteGoti = 9;
			BlackGoti = 9;
			IsQueenAlive = true;
		}
	}
}