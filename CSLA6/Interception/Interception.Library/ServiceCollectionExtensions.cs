using Csla.Server;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{

		public static void AddInjector<T>(this IServiceCollection services) where T : class, IInterceptDataPortal
		{
			services.AddTransient<IInterceptDataPortal, T>();
		}

	}
}
