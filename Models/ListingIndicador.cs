using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_listing_indicador")]
    public class ListingIndicador
    {
        public int CriteriaId { get; set; }
        public int ExecutionId { get; set; }
        public String TipoIndicador { get; set; }
        public double Valor { get; set; } 
    }
}
