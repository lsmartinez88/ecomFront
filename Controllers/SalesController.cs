using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SalesViewModels;
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
    public class SalesController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private IGroupData _groupData;

        public SalesController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IGroupData groupData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _groupData = groupData;
        }

        public IActionResult HomeSales(int? executionId)
        {
            var homeSalesViewModel = new HomeSalesViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homeSalesViewModel.Search = _searchData.GetSearch((int)homeSalesViewModel.Execution.SearchId);
            homeSalesViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeSalesViewModel.Search.IdSearch);
            homeSalesViewModel.SalesPerCityComprador = _searchData.GetSalesMap((int)homeSalesViewModel.Execution.IdExecution,Models.SearchViewModels.SalesType.Compra);
            homeSalesViewModel.SalesPerCityVendedor = _searchData.GetSalesMap((int)homeSalesViewModel.Execution.IdExecution, Models.SearchViewModels.SalesType.Venta);

            return View(homeSalesViewModel);
        }


        [HttpPost]
        public JsonResult GetSalesQttyByDay(int executionId)
        {
            List<AveragePricePerDay> salesItems = _groupData.GetSalesQttyByExecution(executionId);
            var salesQttyByDayViewModel = new SalesQttyByDayViewModel();

            salesItems.ForEach(pi =>
            {
                salesQttyByDayViewModel.items.Add(new SalesQttyByDayInformation { Date = pi.Fecha, Quantity = pi.CantidadVentas });
            });

            salesQttyByDayViewModel.maxValue = salesQttyByDayViewModel.items.Max(si => si.Quantity);
            salesQttyByDayViewModel.minValue = salesQttyByDayViewModel.items.Min(si => si.Quantity);

            return Json(salesQttyByDayViewModel);
        }

        [HttpPost]
        public JsonResult GetEventsByRange(string from, string to)
        {
            DateTime desde = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime hasta = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<Event> Events = _groupData.GetEventsByRange(desde, hasta);
           
            return Json(Events);
        }
    }
}
