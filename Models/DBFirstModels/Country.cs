using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public string IdCountry { get; set; }
        public long Version { get; set; }
        public string Currency { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string Name { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
