using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.TrendViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ecomFront.Controllers
{
    [Authorize]
    public class TrendController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private IGroupData _groupData;

        public TrendController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IGroupData groupData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _groupData = groupData;
        }

        public IActionResult HomeTrend(int? executionId)
        {
            var homeTrendViewModel = new HomeTrendViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homeTrendViewModel.Search = _searchData.GetSearch((int)homeTrendViewModel.Execution.SearchId);
            homeTrendViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeTrendViewModel.Search.IdSearch);
            return View(homeTrendViewModel);
        }

        [HttpPost]
        public JsonResult GetWordCloud(int executionId, int tipo)
        {
            List<WordCloudGrouping> items = _groupData.GetWordCloudByExecution(executionId, tipo);
            var wordCloudViewModel = new WordCloudViewModel();

            if (tipo == WordCloudType.Publicaciones)
            {
                items = items.OrderByDescending(a => a.CantidadPublicaciones).Take(100).ToList();
                items.ForEach(pi =>
                {
                    wordCloudViewModel.items.Add(new WordCloudItem { text = pi.Palabra, weight = pi.CantidadPublicaciones.Value });
                });

            }
            else if (tipo == WordCloudType.Ventas)
            {
                items = items.OrderByDescending(a => a.CantidadVentas).Take(100).ToList();
                items.ForEach(pi =>
                    {
                        wordCloudViewModel.items.Add(new WordCloudItem { text = pi.Palabra, weight = pi.CantidadVentas.Value });
                    });
            }
            else if (tipo == WordCloudType.Oportunidad)
            {
                items = items.Where(a => a.IndicadorOportunidad != null).OrderByDescending(a => a.CantidadVentas).Take(100).ToList();
                items.ForEach(pi =>
                {
                    wordCloudViewModel.items.Add(new WordCloudItem { text = pi.Palabra, weight = pi.IndicadorOportunidad.Value });
                });
            }


            return Json(wordCloudViewModel);
        }


        [HttpPost]
        public JsonResult GetTrendTreemap(int executionId)
        {
            List<TrendsTreemap> items = _groupData.GetTrendsTreemapByExecution(executionId);
            var trendViewModel = new TrendTreemapViewModel();

            trendViewModel.items.Add(new TrendTreemapItem { Trend = "Oportunidad", Parent = "Árbol Tendencias", Size = 0, Color = 1, CantidadVentas = null, CantidadPublicaciones = null, PosicionTendencia = null, VentasPorPublicacion = null });
            trendViewModel.items.Add(new TrendTreemapItem { Trend = "Tendencia", Parent = "Árbol Tendencias", Size = 0, Color = 5, CantidadVentas = null, CantidadPublicaciones = null, PosicionTendencia = null, VentasPorPublicacion = null });
            items.ForEach(pi =>
            {
                trendViewModel.items.Add(new TrendTreemapItem { Trend = pi.TrendName, Parent = pi.TrendPadre, Size = pi.IndiceSize, Color = pi.IndiceColor, CantidadVentas = pi.CantidadVentas, CantidadPublicaciones = pi.CantidadPublicaciones, PosicionTendencia = pi.PosicionTendencia, VentasPorPublicacion = pi.VentasPorPublicacion });
            });

            return Json(trendViewModel);
        }
    }
}
