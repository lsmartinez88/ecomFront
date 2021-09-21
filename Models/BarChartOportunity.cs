using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_bar_chart_oportunity")]
    public class BarChartOportunity
    {
        public int ExecutionId { get; set; }
        public String Palabra { get; set; }
        public double? IndicadorOportunidad { get; set; }
        public int? CantidadAparicionesTendencia { get; set; }
        public int? PosicionMejorTendencia { get; set; }
    }
}
