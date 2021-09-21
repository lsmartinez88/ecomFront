using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_top_sellers_info")]
    public class TopSellersInfo
    {
        public int ExecutionId { get; set; }
        public String SellerId { get; set; }
        public String SellerName { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadPublicaciones { get; set; }
        public int CantidadPreguntas { get; set; }
        public int CantidadVisitas { get; set; }
        public int CantidadReviews { get; set; }
        public String ZonaMayorVenta { get; set; }
        public int ZonaMayorVentaCantidad { get; set; }
        public String PublicacionMayorVenta { get; set; }
        public int PublicacionMayorVentaCantidad { get; set; }
        public int CancelacionesHistorico { get; set; }
        public double RatingNegativo { get; set; }
        public double RatingPositivo { get; set; }
        public double RatingNeutral { get; set; }
        public double ClaimsRate { get; set; }
        public int ClaimsValue { get; set; }
        public double DelayedRate { get; set; }
        public int DelayedValue { get; set; }
        public double CancellationRate { get; set; }
        public int CancellationValue { get; set; }
        public String TipoVendedor { get; set; }
        public int PosicionVendedor { get; set; }
        public double TasaConversion { get; set; }

    }
}
