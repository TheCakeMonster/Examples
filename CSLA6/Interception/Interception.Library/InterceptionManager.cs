using Csla.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interception.Library
{
	public class InterceptionManager
	{
		private readonly IReadOnlyList<IInterceptDataPortal> _interceptors;

		public InterceptionManager(IEnumerable<IInterceptDataPortal> interceptors)
		{
			_interceptors = new List<IInterceptDataPortal>(interceptors);
		}

		public void Initialize(InterceptArgs e)
		{
			foreach (IInterceptDataPortal interceptor in _interceptors)
			{
				interceptor.Initialize(e);
			}
		}

		public void Complete(InterceptArgs e)
		{
			for (int interceptorIndex = _interceptors.Count - 1; interceptorIndex > -1; interceptorIndex--)
			{
				IInterceptDataPortal interceptor = _interceptors[interceptorIndex];
				interceptor.Complete(e);
			}
		}

	}
}
