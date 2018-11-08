namespace CarromCore
{
	internal class CarromRules : ICarromRules
	{
		private readonly IMatch _match;

		public CarromRules(IMatch match)
		{
			_match = match;
		}

		public bool IsMoveValid(IMove move)
		{
			throw new System.NotImplementedException();
		}
	}
}