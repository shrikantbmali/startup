using System;

namespace DMF.Core
{
	public interface ITransaction
	{
		DateTime TransactionDateTime { get; set; }

		DateTime EntryDateTime { get; set; }

		double Amount { get; set; }
	}
}