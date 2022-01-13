using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace VehicleTracker.DataAccess
{

	public static class TransactionScopeFactory
	{

		public static TransactionScope CreateSuppressedReadCommittedTransaction()
		{
			TransactionOptions options = new TransactionOptions();

			options.IsolationLevel = IsolationLevel.ReadCommitted;
			options.Timeout = TimeSpan.FromSeconds(60);
			return new TransactionScope(TransactionScopeOption.Suppress, options, TransactionScopeAsyncFlowOption.Enabled);
		}

	}

}
