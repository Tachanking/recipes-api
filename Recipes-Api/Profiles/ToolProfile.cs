using AutoMapper;
using Recipes_Api.Dto;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class ToolProfile : Profile
    {
        public ToolProfile()
        {
            CreateMap<Tool, ToolDto>().ForMember(dto => dto.Quantity, opt => opt.MapFrom(r => r.RecipeTool.Select(i => i.Quantity).FirstOrDefault()));
            CreateMap<ToolDto, Tool>();
        }
    }
}
