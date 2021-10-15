using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.PricingViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ecomFront.Controllers
{
    [Authorize]
    public class PricingController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private IGroupData _groupData;

        public PricingController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IGroupData groupData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _groupData = groupData;
        }

        public IActionResult HomePricing(int? executionId)
        {
            var homePricingViewModel = new HomePricingViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homePricingViewModel.Search = _searchData.GetSearch((int)homePricingViewModel.Execution.SearchId);
            homePricingViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homePricingViewModel.Search.IdSearch);
            return View(homePricingViewModel);
        }

        [HttpPost]
        public JsonResult GetGroupingPriceMP(int executionId)
        {
            List<PriceRangeGrouping> priceInfo = _groupData.GetPriceRangeGroupingByExecution(executionId, GroupingType.PaymentMethod);
            var scatterPunchInformation = new ScatterPunchInformation
            {
                xName = "Rango",
                yName = "Medio de Pago",
                dataName = "Publicaciones"
            };

            var xRanges = priceInfo.Select(pi => new { pi.RangoDesde, pi.RangoHasta }).Distinct().OrderBy(pi => pi.RangoDesde).ToList();
            xRanges.ToList().ForEach(range => scatterPunchInformation.xList.Add($"{range.RangoDesde} a {range.RangoHasta}"));

            var yRanges = priceInfo.Select(pi => new { pi.ItemGrouping.NamemlShort, pi.ItemGroupingId }).OrderBy(pi => pi.ItemGroupingId).Distinct().ToList();
            yRanges.ToList().ForEach(range => scatterPunchInformation.yList.Add($"{range.NamemlShort}"));


            scatterPunchInformation.data = "[";
            for (int x = 0; x < xRanges.Count(); x++)
            {
                for (int y = 0; y < yRanges.Count(); y++)
                {
                    var pi = priceInfo.FirstOrDefault(pi => pi.RangoDesde == xRanges[x].RangoDesde && pi.RangoHasta == xRanges[x].RangoHasta && pi.ItemGroupingId == yRanges[y].ItemGroupingId);
                    scatterPunchInformation.data += $"[{y},{x},{pi.CantidadPublicaciones}],";
                }
            }
            scatterPunchInformation.data = scatterPunchInformation.data.Remove(scatterPunchInformation.data.Length - 1, 1);
            scatterPunchInformation.data += "]";

            scatterPunchInformation.minValue = priceInfo.Min(pi => pi.CantidadPublicaciones);
            scatterPunchInformation.maxValue = priceInfo.Max(pi => pi.CantidadPublicaciones);
            return Json(scatterPunchInformation);
        }

        [HttpPost]
        public JsonResult GetGroupingPriceFE(int executionId)
        {
            var scatterPunchInformation = new ScatterPunchInformation
            {
                xName = "Rango",
                yName = "Forma de Envio",
                dataName = "Publicaciones"
            };

            List<PriceRangeGrouping> shippingInfo = _groupData.GetPriceRangeGroupingByExecution(executionId, GroupingType.ShippingMethod);
            var yRanges = shippingInfo.Select(pi => new { pi.ItemGrouping.NamemlShort, pi.ItemGroupingId }).OrderBy(pi => pi.ItemGroupingId).Distinct().ToList();
            yRanges.ToList().ForEach(range => scatterPunchInformation.yList.Add($"{range.NamemlShort}"));

            var xRanges = shippingInfo.Select(pi => new { pi.RangoDesde, pi.RangoHasta }).Distinct().OrderBy(pi => pi.RangoDesde).ToList();
            xRanges.ToList().ForEach(range => scatterPunchInformation.xList.Add($"{range.RangoDesde} a {range.RangoHasta}"));
            scatterPunchInformation.data = "[";
            for (int x = 0; x < xRanges.Count(); x++)
            {
                for (int y = 0; y < yRanges.Count(); y++)
                {
                    var si = shippingInfo.FirstOrDefault(si => si.RangoDesde == xRanges[x].RangoDesde && si.RangoHasta == xRanges[x].RangoHasta && si.ItemGroupingId == yRanges[y].ItemGroupingId);
                    scatterPunchInformation.data += $"[{y},{x},{si.CantidadPublicaciones}],";
                }
            }
            scatterPunchInformation.data = scatterPunchInformation.data.Remove(scatterPunchInformation.data.Length - 1, 1);
            scatterPunchInformation.data += "]";

            scatterPunchInformation.minValue = shippingInfo.Min(pi => pi.CantidadPublicaciones);
            scatterPunchInformation.maxValue = shippingInfo.Max(pi => pi.CantidadPublicaciones);
            return Json(scatterPunchInformation);
        }



        [HttpPost]
        public JsonResult GetPricePerDayInfo(int executionId)
        {
            List<AveragePricePerDay> priceItems = _groupData.GetAveragePriceByExecution(executionId);
            var pricingChatInformartion = new PricingChatInformartion();

            priceItems.ForEach(pi =>
           {
               pricingChatInformartion.Prices.Add(pi.PrecioMedio);
               pricingChatInformartion.Dates.Add(pi.Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
           });

            return Json(pricingChatInformartion);
        }


        [HttpPost]
        public JsonResult GetCantidadPorRango(int executionId)
        {
            List<PriceRangeGrouping> priceInfo = _groupData.GetPriceRangeGroupingByExecution(executionId, "PRICES_SOLDS");
            var scatterInformation = new SingleAxisScatterInformation();
            var xRanges = priceInfo.Select(pi => new { pi.RangoDesde, pi.RangoHasta }).Distinct().OrderBy(pi => pi.RangoDesde).ToList();
            xRanges.ToList().ForEach(range => scatterInformation.xList.Add($" $ {range.RangoDesde} a $ {range.RangoHasta}"));
            scatterInformation.dataPublicaciones = "[";
            scatterInformation.dataVentas = "[";
            for (int x = 0; x < xRanges.Count(); x++)
            {
                var pi = priceInfo.FirstOrDefault(pi => pi.RangoDesde == xRanges[x].RangoDesde && pi.RangoHasta == xRanges[x].RangoHasta && pi.ItemGroupingId == 1);
                scatterInformation.dataPublicaciones += $"[{x},{pi.CantidadPublicaciones}],";
                scatterInformation.dataVentas += $"[{x},{pi.CantidadVentas}],";
            }
            scatterInformation.dataPublicaciones = scatterInformation.dataPublicaciones.Remove(scatterInformation.dataPublicaciones.Length - 1, 1);
            scatterInformation.dataVentas = scatterInformation.dataVentas.Remove(scatterInformation.dataVentas.Length - 1, 1);
            scatterInformation.dataPublicaciones += "]";
            scatterInformation.dataVentas += "]";

            scatterInformation.maxValuePublicaciones = priceInfo.Max(pi => pi.CantidadPublicaciones);
            scatterInformation.maxValueVentas = priceInfo.Max(pi => pi.CantidadVentas);

            return Json(scatterInformation);
        }
    }
}
