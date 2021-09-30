using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Category
    {
        public Category()
        {
            CategoryAttributes = new HashSet<CategoryAttribute>();
            Criteria = new HashSet<Criterion>();
            Listings = new HashSet<Listing>();
        }

        public string IdCategoryml { get; set; }
        public long Version { get; set; }
        public string FechaUltimaActualizacion { get; set; }
        public string IdCategoryPadre { get; set; }
        public int Level { get; set; }
        public string Linkml { get; set; }
        public string Nameml { get; set; }
        public string Pictureml { get; set; }
        public int? TotalItemsml { get; set; }

        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<Criterion> Criteria { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
    }
}
