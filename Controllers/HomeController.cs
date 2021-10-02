using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ecomFront.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ecomFront.Services;
using ecomFront.Data;
using ecomFront.Models.MLModels;
using ecomFront.Models.DashboardViewModel;
using System.Globalization;

namespace ecomFront.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IGroupData _groupData;
        private ISearchData _searchData;

        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger,
                                IGroupData groupData, ISearchData searchData)
        {
            _signInManager = signInManager;
            _logger = logger;
            _groupData = groupData;
            _searchData = searchData;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var searches = _searchData.GetFullSearches(_signInManager.UserManager.GetUserId(User));
                if(searches.Count == 0 )
                {
                    return LocalRedirect("/Search/HomeAddSearch");
                }

                List<MainDashboard> dashboard = _searchData.GetMainDashboards(_signInManager.UserManager.GetUserId(User));
                List<int> ids = dashboard.Select(a => a.SearchId).Distinct().ToList();
                var homeViewModel = new MainDashboardViewModel();
                MainDashboardItem dashItem;
                foreach (int id in ids)
                {
                    dashItem = new MainDashboardItem();
                    dashItem.Search = _searchData.GetSearchWithExecutions(id);
                    dashItem.CantidadCompetidores = int.Parse(dashboard.Single(a => a.SearchId.Equals(id) && a.ParameterName.Equals("CantidadCompetidores")).ParameterValue);
                    dashItem.CantidadPublicaciones = int.Parse(dashboard.Single(a => a.SearchId.Equals(id) && a.ParameterName.Equals("CantidadPublicaciones")).ParameterValue);
                    dashItem.CantidadVentas = int.Parse(dashboard.Single(a => a.SearchId.Equals(id) && a.ParameterName.Equals("CantidadVentas")).ParameterValue);
                    dashItem.TemperaturaCategoriaPorcentaje = int.Parse(dashboard.Single(a => a.SearchId.Equals(id) && a.ParameterName.Equals("TemperaturaCategoriaPorcentaje")).ParameterValue, CultureInfo.InvariantCulture);
                   
                    if(dashItem.TemperaturaCategoriaPorcentaje > 75)
                    {
                        dashItem.TemperaturaCategoriaLeyenda = "MUY COMPETITIVO";
                        dashItem.TemperaturaCategoriaClase = "bg-danger";

                    } else if(dashItem.TemperaturaCategoriaPorcentaje > 30)
                    {
                        dashItem.TemperaturaCategoriaLeyenda = "EQUILIBRADO";
                        dashItem.TemperaturaCategoriaClase = "bg-yellow";

                    } else
                    {
                        dashItem.TemperaturaCategoriaLeyenda = "MUCHAS OPORTUNIDADES";
                        dashItem.TemperaturaCategoriaClase = "bg-success";

                    }
                    homeViewModel.Items.Add(dashItem);
                }

                return View(homeViewModel);
            }


            return LocalRedirect("/Static/HomeStatic");
        }

        [HttpPost]
        public JsonResult GetSellersType(int searchId)
        {
            List<MainDashboard> items = _searchData.GetSellersTypeSearch(searchId);
            return Json(items);
        }

        [HttpPost]
        public JsonResult GetAvgPriceLine(int searchId)
        {
            List<MainDashboard> avgPrice = _searchData.GetAvgPriceSearch(searchId);
            List<MainDashboard> avgPriceSell = _searchData.GetAvgSellPriceSearch(searchId);
            var homeViewModel = new AveragePriceLineChart();

            List<int> x = avgPrice.Select(a => int.Parse(a.ParameterName.Replace("EjecucionPrecioMedio_", ""))).ToList();

            x = x.OrderBy(s => s).ToList();
            //homeViewModel.xList = avgPrice.Select(a => a.ParameterName.Replace("EjecucionPrecioMedio_", "")).ToList();
            homeViewModel.xList = x.Select(s => s.ToString()).ToList();
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";

            foreach (var item in avgPrice)
            {
                item.ParameterName = item.ParameterName.Replace("EjecucionPrecioMedio_", "");
            }

            foreach (var item in avgPriceSell)
            {
                item.ParameterName = item.ParameterName.Replace("EjecucionPrecioMedioVenta_", "");
            }

            avgPrice = avgPrice.OrderBy(a => int.Parse(a.ParameterName)).ToList();
            avgPriceSell = avgPriceSell.OrderBy(a => int.Parse(a.ParameterName)).ToList();

            homeViewModel.dataLegend.Add("Precio medio publicación");
            homeViewModel.dataLegend.Add("Precio medio venta");
            homeViewModel.dataSeries.Add(new AveragePriceLineSeries() { legend = "Precio medio publicación", yList = avgPrice.Select(a => a.ParameterValue).ToList() });
            homeViewModel.dataSeries.Add(new AveragePriceLineSeries() { legend = "Precio medio venta", yList = avgPriceSell.Select(a => a.ParameterValue).ToList() });

            return Json(homeViewModel);
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return LocalRedirect("/");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
