using FluentValidation;
using Mongraw.Katalog.Application.Mapper;
using Mongraw.Katalog.Application.Validations;

namespace Mongraw.Katalog.Web.Extensions
{
    public static class MapperDependencyInjection
    {
        public static IServiceCollection MappingRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ItemMapProfile));
            return services;
        }
    }
}
