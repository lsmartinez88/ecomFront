using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.AnalisisViewModels
{
    public class HomeAnalisisViewModel
    {
        public HomeAnalisisViewModel()
        {
        }
        public Search Search{ get; set; }
        public Execution Execution { get; set; }
        public List<Criterion> Criteria{ get; set; }
        public List<ListingIndicador> Indicadores { get; set; }
    }
}
