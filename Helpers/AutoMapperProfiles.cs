using AutoMapper;
using DataTransferObject.OrderDto;
using DataTransferObject.OrderItemDto;
using DataTransferObject.UserDto;
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

            CreateMap<OrderItemForInsertDto, OrderItem>();

            CreateMap<Order, OrderForList>();
            CreateMap<OrderToInsertDto, Order>()
                .ForMember(dest => dest.User,
                opt => opt.MapFrom(x => new User { Id = x.UserId }));
            
        }
    }
}
