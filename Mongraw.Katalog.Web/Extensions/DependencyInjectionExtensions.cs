using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Repositories;
using System.Reflection;

namespace Mongraw.Katalog.Web.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterByConvention(
        this IServiceCollection services,
        string implementationAssemblyName,
        string interfaceNamespace,
        ServiceLifetime lifetime)
        {
            try
            {
                var implementationAssembly = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .FirstOrDefault(a => a.GetName().Name == implementationAssemblyName)
                            ?? Assembly.Load(implementationAssemblyName);

                var typesToRegister = implementationAssembly.GetTypes()
                            .Where(t =>
                                t.IsClass &&
                                !t.IsAbstract &&
                                !t.IsGenericTypeDefinition 
                            )
                            .ToList();

                foreach (var implType in typesToRegister)
                {
                    var interfaceType = implType.GetInterface($"I{implType.Name}");
                    if (interfaceType == null)
                        continue;

                    if (interfaceType.Namespace != null && interfaceType.Namespace.StartsWith(interfaceNamespace))
                    {
                        services.Add(new ServiceDescriptor(interfaceType, implType, lifetime));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services.RegisterByConvention(
                implementationAssemblyName: "Mongraw.Katalog.Repositories",
                interfaceNamespace: "Mongraw.Katalog.Domain.Interfaces",
                lifetime: ServiceLifetime.Scoped
            );
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.RegisterByConvention(
                implementationAssemblyName: "Mongraw.Katalog.Application",
                interfaceNamespace: "Mongraw.Katalog.Application.Service.Interfaces",
                lifetime: ServiceLifetime.Scoped
            );
        }

        public static IServiceCollection RegisterGenericMethods(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}