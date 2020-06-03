using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Tools
    {
        public Tools()
        {
            RecipeTools = new HashSet<RecipeTools>();
        }

        public int ToolId { get; set; }
        public string ToolName { get; set; }

        public virtual ICollection<RecipeTools> RecipeTools { get; set; }
    }
}
