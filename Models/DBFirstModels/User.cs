using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class User
    {
        public User()
        {
            Searches = new HashSet<Search>();
        }

        public long IdUser { get; set; }
        public long Version { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Search> Searches { get; set; }
    }
}
