using System;
using Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
	public static class ConfigureCoreServicesForSingleton
	{
        public static IServiceCollection AddCoreServicesForSingleton(this IServiceCollection services, DataContext context)
        {

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddSingleton(typeof(IRepositoryScopeFactory<>), typeof(RepositoryScopeFactory<>));
            return services;
        }
	}
}

