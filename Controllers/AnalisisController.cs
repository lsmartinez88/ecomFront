using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
using ecomFront.Models.ListingViewModels;
using ecomFront.Models.PricingViewModels;
using ecomFront.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecomFront.Controllers
{
    [Authorize]
    public class AnalisisController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private IGroupData _groupData;
        private IListingData _listingData;

        public AnalisisController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IGroupData groupData, IListingData listingData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _groupData = groupData;
            _listingData = listingData;
        }

        public IActionResult HomeAnalisis(int? executionId)
        {
            var homeAnalisisListViewModel = new HomeAnalisisViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homeAnalisisListViewModel.Search = _searchData.GetSearch((int)homeAnalisisListViewModel.Execution.SearchId);
            homeAnalisisListViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeAnalisisListViewModel.Search.IdSearch);
            homeAnalisisListViewModel.Indicadores = _groupData.GetIndicadorByExecution((int)executionId);

            homeAnalisisListViewModel.Words = _groupData.GetBarChartOportunityByExecution((int)executionId).Take(10).ToList();
            homeAnalisisListViewModel.Trends = _groupData.GetTrendsTreemapByExecution((int)executionId).OrderByDescending(a => a.IndiceSize).Take(10).ToList();
            homeAnalisisListViewModel.EventsIndicator = _groupData.GetEventsIndicatorByExecution((int)executionId);
            return View(homeAnalisisListViewModel);
        }

        [HttpPost]
        public JsonResult GetGroupingInfoPaymentMethod(int executionId)
        {
            List<ListingGrouping> groupInfo = _groupData.GetGroupingByExecution(executionId, GroupingType.PaymentMethod);            
            return Json(groupInfo.Select(gi => new { clasification = gi.ItemGrouping.GroupDescription, qtty = gi.Quantity }).ToList());
        }

        [HttpPost]
        public JsonResult GetGroupingInfoShippingMethod(int executionId)
        {
            List<ListingGrouping> groupInfo = _groupData.GetGroupingByExecution(executionId, GroupingType.ShippingMethod);
            return Json(groupInfo.Select(gi => new { clasification = gi.ItemGrouping.GroupDescription, qtty = gi.Quantity }).ToList());
        }

        [HttpPost]
        public JsonResult GetGroupingInfoFromCategories(int executionId, int criteriaId, string groupingType)
        {
            List<ListingGrouping> groupInfo = _groupData.GetGrouping(criteriaId, executionId, groupingType);
            return Json(groupInfo.Select(gi => new { clasification = gi.ItemGrouping.GroupDescription, qtty = gi.Quantity }).ToList());            
        }

        public IActionResult GrouppingCategories(int executionId, string grouping)
        {
            var grouppingCategoriesViewModel = new GrouppingCategoriesViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            grouppingCategoriesViewModel.Search = _searchData.GetSearch((int)grouppingCategoriesViewModel.Execution.SearchId);
            grouppingCategoriesViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)grouppingCategoriesViewModel.Search.IdSearch);
            grouppingCategoriesViewModel.GroupingMethod = grouping;

            return View(grouppingCategoriesViewModel);
        }


        [HttpPost]
        public JsonResult GetFunnelData(int executionId)
        {
            var homeAnalisisListViewModel = new HomeAnalisisViewModel();
            homeAnalisisListViewModel.Indicadores = _groupData.GetIndicadorFunnelByExecution((int)executionId);
            return Json(homeAnalisisListViewModel.Indicadores);
        }


        [HttpPost]
        public JsonResult GetListingData(int executionId, int listadoType)
        {
            var viewModel = new ListingExecutionList();
            viewModel.Items = _listingData.GetListingByCondition((int)executionId, listadoType).Take(7).ToList();
            foreach (var item in viewModel.Items)
            {
                var pics = MLService.GetItemById(item.IdMl).pictures;
                if(pics != null)
                    item.Pictures = pics.ToList();
            }
            return Json(viewModel);
        }


    }
}
