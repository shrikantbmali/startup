using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarromCore.Tests.Carrom_Tests
{
	[TestClass]
	public class Carrom_On_Instantiation : Carrom_Test_Base
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Should_Not_Accept_Teams_With_Same_ID()
		{
			var carrom = new Carrom(new Team(1, new Player(1, "1"), new Player(2, "2")),
				new Team(1, new Player(1, "1"), new Player(2, "2")));
		}

		[TestMethod]
		public void Should_Accept_Teams_With_Differnet_ID()
		{
			try
			{
				var carrom = CreateCarrom();
			}
			catch (Exception)
			{
				Assert.Fail("Exception was thrown unnecessarily!");
			}
		}

		[TestMethod]
		public void Number_Of_White_Goti_Should_Be_9_Black_Goti_Should_Be_8()
		{
			var carrom = CreateCarrom();

			Assert.AreEqual(9, carrom.WhiteGoti, "White Gotis are not NINE!");
			Assert.AreEqual(8, carrom.BlackGoti, "White Gotis are not NINE!");
		}

		[TestMethod]
		public void Queen_Should_Be_Alive()
		{
			var carrom = CreateCarrom();

			Assert.IsTrue(carrom.IsQueenAlive, "Queen should be alive at the start of the game!");
		}
	}
}
