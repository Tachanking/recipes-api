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

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
