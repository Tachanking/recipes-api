using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Tool
    {
        public Tool()
        {
            RecipeTool = new HashSet<RecipeTool>();
        }

        public int ToolId { get; set; }
        public string ToolName { get; set; }

        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
