using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public AnalisisController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IGroupData groupData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _groupData = groupData;
        }

        public IActionResult Home(int? executionId)
        {
            var homeAnalisisListViewModel = new HomeAnalisisViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homeAnalisisListViewModel.Search = _searchData.GetSearch((int)homeAnalisisListViewModel.Execution.SearchId);
            homeAnalisisListViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeAnalisisListViewModel.Search.IdSearch);
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
    }
}
