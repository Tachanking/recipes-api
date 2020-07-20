using AutoMapper;
using Recipes_Api.Dto;

namespace Recipes_Api.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
        }
    }
}
