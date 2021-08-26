using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class SalesQuantityMap
    {
        public int ExecutionId { get; set; }
        public int CriteriaId { get; set; }
        public String City { get; set; }
        public String Latitud { get; set; }
        public String Longitud { get; set; }
        public SalesType Tipo { get; set; }
        public int Cantidad { get; set; }

    }

    public enum SalesType
    {
        Compra = 0,
        Venta = 1
    }
}
