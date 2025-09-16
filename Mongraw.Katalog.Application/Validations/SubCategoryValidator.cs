using FluentValidation;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Validations
{
    public class SubCategoryValidator : AbstractValidator<Subcategory>
    {
        public SubCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nazwa podkategorii nie może być pusta.");
        }
    }
}
