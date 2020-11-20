using AutoMapper;
using DataTransferObject.OrderDto;
using DataTransferObject.OrderItemsDto;
using DataTransferObject.SimpleDto;
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

            CreateMap<CategoryDto, Category>();
            CreateMap<ColorDto, Color>();
            CreateMap<GlassPackageDto, GlassPackage>();
            CreateMap<QualityDto, Quality>();
            CreateMap<SeriesDto, Series>();

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
