using Microsoft.AspNetCore.Mvc;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Web.Controllers
{
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet("getSubCategoriesByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int categoryId)
        {
            var subCategories = await _subCategoryService.GetSubCategoryByIdAsync(categoryId);
            if(subCategories.IsFailure)
            {
                return NotFound(subCategories.Error);
            }
            return Ok(subCategories.Value);
        }

        [HttpGet("getAllSubCategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await _subCategoryService.GetAllSubCategoriesAsync();
            if (subCategories.IsFailure)
            {
                return NotFound(subCategories.Error);
            }
            return Ok(subCategories.Value);
        }
        [HttpPost("addSubCategory")]
        public async Task<IActionResult> AddSubCategory([FromBody] Subcategory subcategory)
        {
            if (subcategory == null)
            {
                return BadRequest();
            }
            var result = await _subCategoryService.AddSubCategoryAsync(subcategory);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetSubCategoriesByCategoryId), new { id = subcategory.Id }, subcategory);
        }
        [HttpPut("updateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] Subcategory subcategory)
        {
            if (subcategory == null || subcategory.Id != id)
            {
                return BadRequest();
            }
            var result = await _subCategoryService.UpdateSubCategoryAsync(subcategory);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
        [HttpDelete("deleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var result = await _subCategoryService.DeleteSubCategoryAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

    }
}
