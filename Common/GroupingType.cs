using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Common
{
    public static class GroupingType
    {
        public const string
            PaymentMethod = "PAYMENT_METHODS",
            ShippingMethod = "SHIPPING_METHODS";

        private const string
            PaymentMethodName = "Metodos de Pago",
            ShippingMethodName = "Formas de Envio";

        public static string GetName(string groupingType)
        {
            switch (groupingType)
            {
                case PaymentMethod:
                    return PaymentMethodName;
                case ShippingMethod:
                    return ShippingMethodName;
                default:
                    return ""; 
            }            
        }
    }
}
