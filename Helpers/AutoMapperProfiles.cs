using AutoMapper;
using DataTransferObject.OrderDto;
using DataTransferObject.OrderItemsDto;
using DataTransferObject.SimpleDto;
using DataTransferObject.TypologyDtos;
using DataTransferObject.TypologyModelDtos;
using DataTransferObject.UserDto;
using DataTransferObject.UserLogDto;
using Domain;
using System;

namespace Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserDetailDto>();

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ColorDto, Color>().ReverseMap();
            CreateMap<GlassPackageDto, GlassPackage>().ReverseMap();
            CreateMap<QualityDto, Quality>().ReverseMap();
            CreateMap<SeriesDto, Series>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
            CreateMap<Guide, GuideDto>().ReverseMap();
            CreateMap<GlassQuality, GlassQualityDto>().ReverseMap();
            CreateMap<Tabakera, TabakeraDto>().ReverseMap();

            CreateMap<Typology, TypologyDto>().ReverseMap();
            CreateMap<TypologyModel, TypologyModelDto>()
                .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(x => "http://localhost:5000/Uploaded_Documents/" + x.PhotoUrl));


            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Order, OrderForList>();
            CreateMap<OrderToInsertDto, Order>()
                .ForMember(dest => dest.User,
                opt => opt.MapFrom(x => new User { Id = x.UserId }));
            CreateMap<OrderToUpdateDto, Order>();
            
            CreateMap<UserLog, UserLogListDto>();
        }
    }
}
