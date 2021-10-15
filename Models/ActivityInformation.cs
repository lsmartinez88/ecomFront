using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ecomFront.Models
{
    [Table("front_activity_information")]

    public class ActivityInformation
    {
        public int id { get; set; } 
        public string TipoActividad { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
        public ApplicationUser user { get; set; }
    }
}
