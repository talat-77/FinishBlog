using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.BlogEntities
{
    public class ThemePreference : BaseEntity
    {
        public string ThemeName { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
