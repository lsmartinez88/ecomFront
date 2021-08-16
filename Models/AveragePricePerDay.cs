using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_average_price_per_day")]
    public class AveragePricePerDay
    {
        public int CriteriaId { get; set; }
        public int ExecutionId { get; set; }
        public DateTime Fecha { get; set; }
        public double PrecioMedio { get; set; }
        public int CantidadVentas { get; set; }

    }
}
