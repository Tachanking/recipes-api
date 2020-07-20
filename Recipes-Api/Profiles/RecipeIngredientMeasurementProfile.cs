using AutoMapper;
using Recipes_Api.Dto;
using Recipes_Api.Models;

namespace Recipes_Api.Profiles
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
