using AutoMapper;
using SimpleStore.Application.Models;
using SimpleStore.Web.Areas.Store.ViewModels;

namespace SimpleStore.Web.Mappings
{
    public class StoreViewModelsMappingProfile : Profile
    {
        public StoreViewModelsMappingProfile()
        {
            CreateMap<ItemModel, ItemViewModel>()
                .ForMember(
                    e => e.ItemId,
                    e => e.MapFrom(e => e.ItemId))
                .ReverseMap();

            CreateMap<CharacteristicModel, CharacteristicViewModel>()
                .ForMember(
                    destination => destination.CharacteristicId,
                    options => options.MapFrom(
                        source => source.CharacteristicId))
                .ReverseMap();

            CreateMap<CartItemViewModel, OrderDetailModel>()
                .ForPath(
                    destination => destination.Item.ItemId,
                    options => options.MapFrom(
                        source => source.Item.ItemId))
                //.ForMember(
                //    destination => destination.Item.ItemId,
                //    options => options.MapFrom(
                //        source => source.Item.ItemId))
                .ForMember(
                    destination => destination.Quantity,
                    options => options.MapFrom(
                        source => source.Quantity))
                .ReverseMap();

            CreateMap<CategoryModel, CategoryViewModel>()
                .ReverseMap();
        }
    }
}
