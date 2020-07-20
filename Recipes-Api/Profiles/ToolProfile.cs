using AutoMapper;
using Recipes_Api.Dto;

namespace Recipes_Api.Profiles
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
