using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class User
    {
        public string IdUser { get; set; }
        public long Version { get; set; }
        public string Name { get; set; }
    }
}
