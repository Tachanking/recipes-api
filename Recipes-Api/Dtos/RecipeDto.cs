﻿using System.Collections.Generic;

namespace Recipes_API.Dto
{
    public class RecipeDto
    {
        public string Name { get; set; }

        public ICollection<RecipeIngredientMeasurementDto> RecipeIngredientMeasurements;
        public ICollection<RecipeToolDto> RecipeTools;
    }
}