using AutoMapper;
using Recipes_API.Dto;

namespace Recipes_API.Profiles
{
    public class ToolProfile : Profile
    {
        public ToolProfile()
        {
            CreateMap<Tool, ToolDto>();
            CreateMap<ToolDto, Tool>();
        }
    }
}
