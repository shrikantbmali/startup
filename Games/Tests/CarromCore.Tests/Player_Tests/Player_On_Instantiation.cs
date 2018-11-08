using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarromCore.Tests.Player_Tests
{
	[TestClass]
	public class Player_On_Instantiation
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Should_Throw_Exception_For_Invalid_Alias_Name()
		{
			new Player(1, string.Empty);
		}

		[TestMethod]
		public void Provided_Id_And_Alias_Should_Be_Assigned()
		{
			const string alias = "Alias";
			const uint id = 1;

			var player = new Player(id, alias);

			Assert.AreEqual(id, player.ID, "ID provided is not assigned properly!");
			Assert.AreEqual(alias, player.Alias, "Alias provided is not assigned properly!");
		}
	}
}