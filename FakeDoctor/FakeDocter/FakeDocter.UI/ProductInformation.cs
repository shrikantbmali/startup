using System;

namespace FakeDocter.UI
{
	public struct ProductInformation
	{
		public int Index { get; set; }

		public int Quantity { get; set; }

		public string Name { get; set; }

		public string BatchNumber { get; set; }

		public DateTime ExpiryDate { get; set; }

		public double Amount { get; set; }
	}
}