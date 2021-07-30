using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.SearchViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ecomFront.Controllers
{
    [Authorize]
    public class AnalisisController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;

        public AnalisisController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
        }

        public IActionResult Home(int? executionId)
        {
            var homeAnalisisListViewModel = new HomeAnalisisViewModel();
            homeAnalisisListViewModel.Execution = _searchData.GetExecution(executionId);
            homeAnalisisListViewModel.Search = _searchData.GetSearch((int)homeAnalisisListViewModel.Execution.SearchId);
            homeAnalisisListViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeAnalisisListViewModel.Search.IdSearch);
            return View(homeAnalisisListViewModel);
        }
    }
}
