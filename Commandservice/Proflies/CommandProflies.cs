using AutoMapper;
using Commandservice.Dtos;
using Commandservice.Models;

namespace Commandservice.Proflies
{
    public class CommandProflies : Profile
    {
        public CommandProflies() 
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
