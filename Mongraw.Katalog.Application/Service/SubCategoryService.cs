using CSharpFunctionalExtensions;
using FluentValidation;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IValidator<Subcategory> _subCategoryValidator;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository, IValidator<Subcategory> subCategoryValidator)
        {
            _subCategoryRepository = subCategoryRepository;
            _subCategoryValidator = subCategoryValidator;
        }

        public async Task<Result<IEnumerable<Subcategory>>> GetAllSubCategoriesAsync()
        {
            var result = await _subCategoryRepository.GetAllSubCategoriesAsync();
            return Result.Success(result);
        }

        public async Task<Result<Subcategory?>> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
            {
                var errorText = Resources.ItemIsMissing;
                return Result.Failure<Subcategory?>(string.Format(errorText, "podkategorii"));
            }
            return Result.Success(subCategory);
        }

        public async Task<Result> AddSubCategoryAsync(Subcategory subcategory)
        {
            var validationResult = _subCategoryValidator.Validate(subcategory);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errorMessages);
            }
            await _subCategoryRepository.AddSubCategoryAsync(subcategory);
            return Result.Success();
        }

        public async Task<Result> UpdateSubCategoryAsync(Subcategory subcategory)
        {
            var validationResult = _subCategoryValidator.Validate(subcategory);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errorMessages);
            }
            var existingSubCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subcategory.Id);
            if (existingSubCategory == null)
            {
                var errorText = Resources.ItemIsMissing;
                return Result.Failure(string.Format(errorText, "podkategorii"));
            }
            await _subCategoryRepository.UpdateSubCategoryAsync(subcategory);
            return Result.Success();
        }

        public async Task<Result> DeleteSubCategoryAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
            {
                var errorText = Resources.ItemIsMissing;
                return Result.Failure(string.Format(errorText, "podkategorii"));
            }
            if (subCategory.Items != null && subCategory.Items.Any())
            {
                var errorText = Resources.DeleteIsNotPossible;
                return Result.Failure(string.Format(errorText, "podkategorii", "produkty"));
            }
            await _subCategoryRepository.DeleteSubCategoryAsync(subCategory);
            return Result.Success();
        }
    }
}
