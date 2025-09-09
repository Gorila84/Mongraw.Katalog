
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Mongraw.Katalog.Web.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services.RegisterByConvention("Mongraw.Katalog.Repositories", "Repository", ServiceLifetime.Scoped);
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.RegisterByConvention("Mongraw.Katalog.Application", "Service", ServiceLifetime.Scoped);
        }

        private static IServiceCollection RegisterByConvention(
            this IServiceCollection services,
            string assemblyName,
            string endsWith,
            ServiceLifetime lifetime)
        {
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == assemblyName);

            if (assembly == null)
                throw new InvalidOperationException($"Assembly '{assemblyName}' not found.");

            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(endsWith))
                .ToList();

            foreach (var implementationType in types)
            {
                var interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
                if (interfaceType != null)
                {
                    services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));
                }
            }

            return services;
        }
    }
}
