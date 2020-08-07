using AutoMapper;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>().ForMember(dto => dto.Ingredients, opt => opt.MapFrom(r => r.RecipeIngredientMeasurement.Select(i => i.Ingredient).ToList()))
                                            .ForMember(dto => dto.Tools, opt => opt.MapFrom(t => t.RecipeTool.Select(rt => rt.Tool).ToList()));
            CreateMap<RecipeDto, Recipe>();
        }
    }
}
