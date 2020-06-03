using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class RecipeTool
    {
        public int RecipeId { get; set; }
        public int ToolId { get; set; }
        public int Quantity { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
