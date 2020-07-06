﻿using Recipes_API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes_API
{
    public partial class Measurement
    {
        public Measurement()
        {
            RecipeIngredientMeasurement = new HashSet<RecipeIngredientMeasurement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
    }
}
