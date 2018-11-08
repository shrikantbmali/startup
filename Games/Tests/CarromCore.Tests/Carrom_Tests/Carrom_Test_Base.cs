namespace CarromCore.Tests.Carrom_Tests
{
	public class Carrom_Test_Base
	{
		protected static Carrom CreateCarrom()
		{
			return new Carrom(new Team(1, new Player(1, "1"), new Player(2, "2")),
				new Team(2, new Player(1, "1"), new Player(2, "2")));
		}
	}
}