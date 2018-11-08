using Games.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarromCore.Tests.Team_Tests
{
	[TestClass]
	public class Team_On_Instantiation
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Should_Throw_InvalidTeamException_When_Player_With_Same_ID_Is_Provided()
		{
			new Team(IDProvider.Instance.GenerateID(), new Player(1, "Player 1"), new Player(1, "Player 2"));
		}

		[TestMethod]
		public void Should_Not_Throw_Exception_If_Player_are_With_Differnt_IDs()
		{
			try
			{
				new Team(IDProvider.Instance.GenerateID(), new Player(1, "a"), new Player(2, "b"));
			}
			catch (Exception)
			{
				Assert.Fail("Exception was thrown even when the Player IDs were differnet!");
			}
		}

		[TestMethod]
		public void Should_Assign_ID_As_Passed()
		{
			var generatedID = IDProvider.Instance.GenerateID();
			var team = new Team(generatedID, new Player(1, "a"), new Player(2, "b"));
			Assert.AreEqual(generatedID, team.ID, "ID dose not match!");
		}
	}
}