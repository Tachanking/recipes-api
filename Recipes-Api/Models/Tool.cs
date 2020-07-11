﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes_API
{
    public partial class Tool
    {
        public Tool()
        {
            RecipeTool = new HashSet<RecipeTool>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}