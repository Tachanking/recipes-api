using AutoMapper;
using Recipes_Api.Dto;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>().ForMember(dto => dto.Measurements, opt => opt.MapFrom(x => x.RecipeIngredientMeasurement.Select(y => y.Measurement).ToList()));
            CreateMap<IngredientDto, Ingredient>();
        }
    }
}
