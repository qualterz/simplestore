using AutoMapper;
using SimpleStore.Application.Models;
using SimpleStore.Web.Areas.Administration.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Web.Mappings
{
    public class AdministrationViewModelsMappingProfile : Profile
    {
        public AdministrationViewModelsMappingProfile()
        {
            CreateMap<ItemModel, ItemViewModel>()
                .ForMember(
                    destination => destination.ItemId,
                    options => options.MapFrom(
                        source => source.ItemId))
                .ForMember(
                    destination => destination.Characteristics,
                    options => options.MapFrom(
                        source => source.Characteristics))
                .ReverseMap();

            CreateMap<CharacteristicModel, CharacteristicViewModel>()
                .ForMember(
                    destination => destination.CharacteristicId,
                    options => options.MapFrom(
                        source => source.CharacteristicId))
                .ReverseMap();

            CreateMap<CategoryModel, CategoryViewModel>()
                .ReverseMap();

            CreateMap<OrderModel, OrderViewModel>()
                .ReverseMap();

            CreateMap<OrderDetailModel, OrderDetailViewModel>()
                .ReverseMap();
        }
    }
}
