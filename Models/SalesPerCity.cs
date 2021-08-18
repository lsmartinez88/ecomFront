using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_sales_per_city")]
    public class SalesPerCity
    {
        public int ExecutionId { get; set; }
        public int CriteriaId { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Latitud { get; set; }
        public String Longitud { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadCompras { get; set; }


    }
}
