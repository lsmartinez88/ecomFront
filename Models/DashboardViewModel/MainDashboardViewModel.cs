using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.DashboardViewModel
{
    public class MainDashboardViewModel
    {
        public MainDashboardViewModel()
        {
            Items = new List<MainDashboardItem>();
        }
        public List<MainDashboardItem> Items { get; set; }
    }
}
