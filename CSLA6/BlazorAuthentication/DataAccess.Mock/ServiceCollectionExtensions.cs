using DataAccess;
using DataAccess.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{

		public static IServiceCollection AddMockRepositories(this IServiceCollection services)
		{
			services.AddTransient<IPersonDal, PersonDal>();
			return services;
		}
	}
}
