
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Application.Dto.ItemDtos;
using Mongraw.Katalog.Application.Helpers;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.ItemsEntities;
using System.Linq.Expressions;
using LinqKit;

namespace Mongraw.Katalog.Application.Service
{
    public class ItemService
    {
        private readonly IGenericRepository<Item> _genericRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IValidator<AddItemDto> _validator;
        private readonly IValidator<EditItemDto> _editItemValidator;
        private readonly IMapper _mapper;

        public ItemService(IGenericRepository<Item> genericRepository, IItemRepository itemRepository, IValidator<AddItemDto> validator, IValidator<EditItemDto> editItemValidator, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _itemRepository = itemRepository;
            _validator = validator;
            _editItemValidator = editItemValidator;
            _mapper = mapper;
        }

        public async Task<Result<Item>> GetItemById(int id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            return item is null ? Result.Failure<Item>(string.Format(Resources.ItemIsMissing, id)) : Result.Success(item);
        }

        public async Task<Result<(IEnumerable<Item> Items, int TotalCount)>> GetItems(int pageNumber = 1, int pageSize = 10)
        {

            var (items, totalCount) = await _itemRepository.GetItemsAsync(null, pageNumber, pageSize);
            return Result.Success((items, totalCount));
        }

        public async Task<Result<Item>> CreateItem(AddItemDto item)
        {
            var validations = await _validator.ValidateAsync(item);
            if (!validations.IsValid)
            {
                var errors = string.Join(", ", validations.Errors.Select(e => e.ErrorMessage));
                return Result.Failure<Item>(errors);
            }
            var newItem = _mapper.Map<Item>(item);
            await _itemRepository.AddItemAsync(newItem);
            return Result.Success(newItem);
        }

        public async Task<Result<Item>> UpdateItem(EditItemDto item)
        {
            var existingItem = await _itemRepository.GetItemByIdAsync(item.Id);
            if (existingItem is null)
            {
                return Result.Failure<Item>(string.Format(Resources.ItemIsMissing,item.Id));
            }

           

            var validations = await _editItemValidator.ValidateAsync(item);
            if (!validations.IsValid)
            {
                var errors = string.Join(", ", validations.Errors.Select(e => e.ErrorMessage));
                return Result.Failure<Item>(errors);
            }
            var newItem = _mapper.Map(item, existingItem);
       
            await _itemRepository.UpdateItemAsync(newItem);
            return Result.Success(newItem);
        }

        public async Task<PageResult<ItemListDto>> FindItemsAsync(FindItemsParams parameters)
        {
            if (parameters.PageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters.PageNumber));

            if (parameters.PageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters.PageSize));


            var predicate = PredicateBuilder.New<Item>(true);

            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                predicate = predicate.And(i => i.Name.Contains(parameters.Name));
            }
            if (parameters.CategoryId.HasValue)
            {
                predicate = predicate.And(i => i.CategoryId == parameters.CategoryId.Value);
            }
            if (parameters.SubcategoryId.HasValue)
            {
                predicate = predicate.And(i => i.SubcategoryId == parameters.SubcategoryId.Value);
            }
            if (parameters.MinPrice.HasValue)
            {
                predicate = predicate.And(i => i.Price >= parameters.MinPrice.Value);
            }
            if (parameters.MaxPrice.HasValue)
            {
                predicate = predicate.And(i => i.Price <= parameters.MaxPrice.Value);
            }


            var query = _genericRepository.GetQueryable(
                predicate,
                include: q => q.Include(i => i.Images)); 



            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = _mapper.Map<List<ItemListDto>>(items);

            return new PageResult<ItemListDto>
            {
                Items = result,
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }
    }
}
