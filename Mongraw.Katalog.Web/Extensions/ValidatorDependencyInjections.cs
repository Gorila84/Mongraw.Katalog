using FluentValidation;
using Mongraw.Katalog.Application.Validations;

namespace Mongraw.Katalog.Web.Extensions
{
    public static class ValidatorDependencyInjections
    {
        public static IServiceCollection RegisterFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<SubCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<AddItemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<EditItemDtoValidators>();
            return services;
        }
    }
}
