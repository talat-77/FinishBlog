﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.BlogEntities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
