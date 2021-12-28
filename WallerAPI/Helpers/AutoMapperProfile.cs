using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;

namespace WallerAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(u => u.Email));

            CreateMap<User, RegisterSuccessDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));

            CreateMap<User, UserToReturnDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
