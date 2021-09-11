using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_word_cloud_grouping")]
    public class WordCloudGrouping
    {
        public int CriteriaId { get; set; }
        public int ExecutionId { get; set; }
        public String Palabra { get; set; }
        public int? CantidadVentas { get; set; }
        public int? CantidadPublicaciones { get; set; }
        public double? IndicadorOportunidad { get; set; }
    }
}
