using AutoMapper;
using Recipes_Api.Dto;

namespace Recipes_Api.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();
        }
    }
}
