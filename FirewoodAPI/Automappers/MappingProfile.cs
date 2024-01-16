using AutoMapper;
using FirewoodAPI.DTOs;
using FirewoodAPI.Models;

namespace FirewoodAPI.Automappers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Id, m => m.MapFrom(u => u.UserId));
            CreateMap<UserInsertDto, User>();
        }
    }
}
