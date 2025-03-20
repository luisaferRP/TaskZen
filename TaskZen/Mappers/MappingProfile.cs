using AutoMapper;
using TaskZen.DTOs;
using TaskZen.Models;

namespace TaskZen.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
