using CSharpFunctionalExtensions;
using FluentValidation;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<Category> _categoryValidator;

        public ISubCategoryService Object1 { get; }
        public IValidator<Subcategory> Object2 { get; }

        public CategoryService(ICategoryRepository categoryRepository, IValidator<Category> categoryValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
        }

     

        public async Task<Result<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            var result = await _categoryRepository.GetAllCategoriesAsync();
            return Result.Success(result);
        }
        public async Task<Result<Category?>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return Result.Failure<Category?>("Nie znaleziono kategorii o podanym numerze.");
            }
            return Result.Success(category);
        }
        public async Task<Result> AddCategoryAsync(Category category)
        {
            var result = _categoryValidator.Validate(category);

            if (!result.IsValid)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errorMessages);
            }

            await _categoryRepository.AddCategoryAsync(category);
            return Result.Success();
        }
        public async Task<Result> UpdateCategoryAsync(Category category)
        {
            var result = _categoryValidator.Validate(category);

            if (!result.IsValid)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errorMessages);
            }

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
           
            if (existingCategory == null)
            {
                var errorText = Resources.ItemIsMissing;
                return Result.Failure(string.Format(errorText, "kategorii"));
            }
            await _categoryRepository.UpdateCategoryAsync(category);
            return Result.Success();
        }
        public async Task<Result> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                var errorText = Resources.ItemIsMissing;
                return Result.Failure(string.Format(errorText, "kategorii"));
            }

            if (category.Items.Count > 0)
            {
                var errorText = Resources.DeleteIsNotPossible;
                return Result.Failure(string.Format(errorText, "kategorii", "produkty"));
            }

            if (category.Subcategories.Count > 0)
            {
                var errorText = Resources.DeleteIsNotPossible;
                return Result.Failure(string.Format(errorText, "kategorii", "podkategorie"));
            }
            await _categoryRepository.DeleteCategoryAsync(category);
            return Result.Success();
        }
    }
}
