using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.DashboardViewModel
{
    public class MainDashboardItem
    {

        public MainDashboardItem()
        {


        }

        public Search Search { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadCompetidores { get; set; }
        public int CantidadPublicaciones { get; set; }
        public double TemperaturaCategoriaPorcentaje { get; set; }
        public String TemperaturaCategoriaLeyenda { get; set; }
        public String TemperaturaCategoriaClase { get; set; }
    }
}
