using System;
using System.Runtime.Serialization;

namespace VehicleTracker.DataAccess
{

	[Serializable]
	public class IncorrectRowCountException : Exception
	{
		public IncorrectRowCountException()
		{
		}

		public IncorrectRowCountException(string message) : base(message)
		{
		}

		public IncorrectRowCountException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected IncorrectRowCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

}