using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes_API.Data
{
    public class IngredientDataProvider
    {
        public void GetIngredients()
        {
            using (var context = new PostgresContext())
            {
                var ingredients = context.Ingredients.ToList();
            }
        }
    }
}
