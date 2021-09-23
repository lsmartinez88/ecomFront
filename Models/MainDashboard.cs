using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_main_dashboard")]
    public class MainDashboard
    {
        public String UserId { get; set; }
        public int SearchId { get; set; }
        public String ParameterName { get; set; }
        public String ParameterValue { get; set; }
    }
}
