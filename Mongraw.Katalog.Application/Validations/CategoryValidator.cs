using FluentValidation;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Validations
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nazwa kategorii nie może być pusta.")
                .MustAsync(async (name, ct) => !await BeUniqueName(name))
                .WithMessage("Nazwa kategorii musi być unikalna.");
        }

        private async Task<bool> BeUniqueName(string name)
        {
            var existingCategory = await _categoryRepository.ExistsByNameAsync(name);
            return existingCategory == null;
        }
    }
}