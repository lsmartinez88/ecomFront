using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class City
    {
        public string IdCity { get; set; }
        public long Version { get; set; }
        public string IdCityGob { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string Name { get; set; }
        public string StateId { get; set; }

        public virtual State State { get; set; }
    }
}
