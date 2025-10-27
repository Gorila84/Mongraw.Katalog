using AutoMapper;
using Mongraw.Katalog.Application.Dto.ItemDtos;
using Mongraw.Katalog.Domain.Models.ItemsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Application.Mapper
{
    public class ItemMapProfile : Profile
    {
        public ItemMapProfile()
        {
            CreateMap<Item, AddItemDto>().ReverseMap();
            CreateMap<Item, EditItemDto>().ReverseMap();
            CreateMap<Item, ItemListDto>().ReverseMap();
        }
    }
}