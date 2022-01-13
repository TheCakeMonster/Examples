using Csla;
using Csla.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interception.Library.Interceptors
{
	public class Interceptor2 : IInterceptDataPortal
	{

		private readonly ApplicationContext _applicationContext;

		public Interceptor2(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		public void Initialize(InterceptArgs e)
		{
			Debug.WriteLine("Interceptor 2 Initialize");
		}

		public void Complete(InterceptArgs e)
		{
			Debug.WriteLine("Interceptor 2 Complete");
		}

	}
}
