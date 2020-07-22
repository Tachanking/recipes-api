using AutoMapper;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class RecipeIngredientMeasurementProfile : Profile
    {
        public RecipeIngredientMeasurementProfile()
        {
            //CreateMap<RecipeDto, RecipeIngredientMeasurement>().ForMember(dto => dto.RecipeId, opt => opt.MapFrom(r => r.Id))
            //                                                    .ForMember(dto => dto.IngredientId, opt => opt.MapFrom(r => r.Ingredients.Select(i => i.Id)))
            //                                                    .ForMember(dto => dto.MeasurementId, opt => opt.MapFrom(r => r.Ingredients.Select(i => i.Measurements.Select(m => m.Id))))
            //                                                    .ForMember(dto => dto.Quantity, opt => opt.MapFrom(r => r.Ingredients.Select(i => i.Measurements.Select(m => m.Quantity))));
        }
    }
}
