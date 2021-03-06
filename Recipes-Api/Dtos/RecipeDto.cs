﻿using System.Collections.Generic;

namespace Recipes_Api.Dto
{
    public class RecipeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<IngredientDto> Ingredients { get; set; }
        public IList<ToolDto> Tools { get; set; }
    }
}
