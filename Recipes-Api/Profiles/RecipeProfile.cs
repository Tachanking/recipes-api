using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipes_Api.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>().ForMember(dto => dto.Ingredients, opt => opt.MapFrom(r => r.RecipeIngredientMeasurement.Select(i => i.Ingredient).ToList()))
                                            .ForMember(dto => dto.Tools, opt => opt.MapFrom(t => t.RecipeTool.Select(rt => rt.Tool).ToList()));
            CreateMap<RecipeDto, Recipe>().ForMember(e => e.RecipeIngredientMeasurement, opt => opt.MapFrom(r => RecipeDtoToRecipeIngredientMeasurements(r)))
                                            .ForMember(e => e.RecipeTool, opt => opt.MapFrom(r => RecipeDtoToRecipeTools(r)));
        }

        // todo
        private static List<RecipeIngredientMeasurement> RecipeDtoToRecipeIngredientMeasurements(RecipeDto recipeDto)
        {
            var recipeIngredientMeasurements = new List<RecipeIngredientMeasurement>();
            foreach(var ingredientDto in recipeDto.Ingredients)
            {
                foreach(var measurementDto in ingredientDto.Measurements)
                {
                    var recipeIngredientMeasurement = new RecipeIngredientMeasurement()
                    {
                        Ingredient = new Ingredient() { 
                            Id = ingredientDto.Id,
                            Name = ingredientDto.Name
                        },
                        Measurement = new Measurement() { 
                            Id = measurementDto.Id,
                            Name = measurementDto.Name,
                            Symbol = measurementDto.Symbol
                        },

                        Quantity = measurementDto.Quantity
                    };

                    recipeIngredientMeasurements.Add(recipeIngredientMeasurement);
                }
            }

            return recipeIngredientMeasurements;
        }

        private static List<RecipeTool> RecipeDtoToRecipeTools(RecipeDto recipeDto)
        {
            var recipeTools = new List<RecipeTool>();
            foreach(var toolDto in recipeDto.Tools)
            {
                var recipeTool = new RecipeTool()
                {
                    Recipe = new Recipe() 
                    { 
                        Id = recipeDto.Id,
                        Name = recipeDto.Name
                    },
                    Tool = new Tool()
                    {
                        Id = toolDto.Id,
                        Name = toolDto.Name
                    },
                    Quantity = toolDto.Quantity
                };

                recipeTools.Add(recipeTool);
            }

            return recipeTools;
        }
    }
}
