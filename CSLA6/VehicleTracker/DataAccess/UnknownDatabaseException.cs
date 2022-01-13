using System;
using System.Runtime.Serialization;

namespace VehicleTracker.DataAccess
{

	[Serializable]
	public class UnknownDatabaseException : Exception
	{
		public UnknownDatabaseException()
		{
		}

		public UnknownDatabaseException(string message) : base(message)
		{
		}

		public UnknownDatabaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnknownDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

}