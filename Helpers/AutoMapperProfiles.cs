using AutoMapper;
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
        }
    }
}
