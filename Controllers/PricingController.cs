using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.PricingViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

    }
}
