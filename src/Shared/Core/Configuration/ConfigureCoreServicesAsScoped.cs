using System;
using Core.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Configuration
{
    public static class ConfigureCoreServicesAsScoped
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}

