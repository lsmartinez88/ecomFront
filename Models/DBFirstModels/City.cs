using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class City
    {
        public string IdCity { get; set; }
        public long Version { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Name { get; set; }
        public string StateId { get; set; }

        public virtual State State { get; set; }
    }
}
