﻿using System.Collections.Generic;

namespace Recipes_API.Dto
{
    public class RecipeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<RecipeIngredientDto> RecipeIngredients;
        public ICollection<RecipeToolDto> RecipeTools;
    }
}