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
    public class SearchController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SearchController> _logger;
        private ISearchData _searchData;

        public SearchController(UserManager<ApplicationUser> userManager,
                                ILogger<SearchController> logger,
                                ISearchData searchData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
        }

        public IActionResult Searches()
        {
            var userid = 3; //reemplazar por el user asociado al identity

            var searchesVM = new SearchesViewModel
            {
                Searches = _searchData.GetFullSearches(userid)
            };

            return View(searchesVM);
        }

        public IActionResult Criteria(int? searchId)
        {
            var criteriaListViewModel = new CriteriaListViewModel();
            var criterias = _searchData.GetFullCriteriasBySearchId(searchId);
            foreach (var criteria in criterias)
            {
                var cti = new CriteriaItemList(criteria);
                criteriaListViewModel.criteriaItems.Add(cti);
            }
            return View(criteriaListViewModel);
        }
    }
}
