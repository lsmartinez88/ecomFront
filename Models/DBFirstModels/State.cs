using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public string IdState { get; set; }
        public long Version { get; set; }
        public string CountryId { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
