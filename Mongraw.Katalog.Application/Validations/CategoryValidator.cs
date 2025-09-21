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
                .WithName("Nazwa kategorii")
                .WithMessage(Resources.ThisFieldIsRequired)
                .MustAsync(async (name, ct) => !await BeUniqueName(name))
                .WithMessage(Resources.UniqueNameIsRequire);
        }

        private async Task<bool> BeUniqueName(string name)
        {
            var existingCategory = await _categoryRepository.ExistsByNameAsync(name);
            return existingCategory == null;
        }
    }
}