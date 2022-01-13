using Csla.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interception.Library.Interceptors
{
	public class Interceptor1 : IInterceptDataPortal
	{
		public void Initialize(InterceptArgs e)
		{
			Debug.WriteLine("Interceptor 1 Initialize");
		}

		public void Complete(InterceptArgs e)
		{
			Debug.WriteLine("Interceptor 1 Complete");
		}

	}
}
