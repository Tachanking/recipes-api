using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class RecipeTools
    {
        public int RecipeId { get; set; }
        public int ToolId { get; set; }
        public int Quantity { get; set; }

        public virtual Recipes Recipe { get; set; }
        public virtual Tools Tool { get; set; }
    }
}
