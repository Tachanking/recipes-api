using AutoMapper;
using Recipes_API.Dto;

namespace Recipes_API.Profiles
{
    public class RecipeToolProfile : Profile
    {
        public RecipeToolProfile()
        {
            CreateMap<RecipeTool, RecipeToolDto>();
            CreateMap<RecipeToolDto, RecipeTool>();
        }
    }
}
