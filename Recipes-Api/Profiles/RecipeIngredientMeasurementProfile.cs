using AutoMapper;
using Recipes_API.Dto;
using Recipes_API.Models;

namespace Recipes_API.Profiles
{
    public class RecipeIngredientMeasurementProfile : Profile
    {
        public RecipeIngredientMeasurementProfile()
        {
            CreateMap<RecipeIngredientMeasurement, RecipeIngredientMeasurementDto>();
            CreateMap<RecipeIngredientMeasurementDto, RecipeIngredientMeasurement>();
        }
    }
}
