using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Games.Common.Tests
{
	[TestClass]
	public class IDProvider_On_Instantiation
	{
		[TestMethod]
		public void Test_Singleton()
		{
			Assert.IsNotNull(IDProvider.Instance, "Instance NULL, FAIL!");
			Assert.AreSame(IDProvider.Instance, IDProvider.Instance, "Singletop Pattern FAIL!");
		}

		[TestMethod]
		public void Should_Generate_Seqentially_New_Id()
		{
			var id1 = IDProvider.Instance.GenerateID();
			var id2 = IDProvider.Instance.GenerateID();

			Assert.IsTrue(id2 - id1 == 1, "Difference between two sequential generated Ids is not one!");
		}
	}
}