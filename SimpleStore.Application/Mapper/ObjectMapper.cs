using AutoMapper;
using SimpleStore.Application.Models;
using SimpleStore.Core.Entities;
using System;

namespace SimpleStore.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            return new MapperConfiguration(config =>
            {
                config.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                config.AddProfile<ModelMappingProfile>();
            }).CreateMapper();
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<Category, CategoryModel>()
                .ReverseMap();

            CreateMap<Characteristic, CharacteristicModel>()
                .ReverseMap();

            CreateMap<Item, ItemModel>()
                .ForMember(
                    destination => destination.Characteristics,
                    options => options.MapFrom(
                        source => source.ItemCharacteristics))
                .ReverseMap();

            CreateMap<ItemCharacteristic, CharacteristicModel>().
                ForMember(
                    destinaton => destinaton.Name,
                    options => options.MapFrom(
                        source => source.Characteristic.Name))
                .ForMember(
                    destinaton => destinaton.Value,
                    options => options.MapFrom(
                        source => source.Characteristic.Value))
                .ReverseMap();

            CreateMap<Order, OrderModel>()
                .ReverseMap();

            CreateMap<OrderDetail, OrderDetailModel>()
                .ReverseMap();
        }
    }
}
