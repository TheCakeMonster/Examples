using System;
using System.Runtime.Serialization;

namespace VehicleTracker.DataAccess
{

	[Serializable]
	public class UnknownReturnValueException : Exception
	{
		public UnknownReturnValueException()
		{
		}

		public UnknownReturnValueException(string message) : base(message)
		{
		}

		public UnknownReturnValueException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnknownReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

}