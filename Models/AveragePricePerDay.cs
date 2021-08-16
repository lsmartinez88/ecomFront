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
<<<<<<< HEAD
        public int CantidadVentas { get; set; }

=======
>>>>>>> 8ae1ab303453b1b92497606e9c6c0c5484f86e3d
    }
}
