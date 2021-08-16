using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.SearchViewModels;
using ecomFront.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

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
            var userid = _userManager.GetUserId(HttpContext.User);

            var searchesVM = new SearchesViewModel
            {
                Searches = _searchData.GetFullSearches(userid)
            };

            foreach(var search in searchesVM.Searches.Where(s => !string.IsNullOrEmpty(s.ListingPermalink)))
            {
                var searchItem = new SearchRelatedItem();
                searchItem.item = MLService.GetItemByPermaLink(search.ListingPermalink);
                searchItem.searchId = search.IdSearch;
                searchesVM.RelatedItems.Add(searchItem);
            }

            return View(searchesVM);
        }

        public IActionResult Criteria(int? searchId)
        {
            var criteriaListViewModel = new CriteriaListViewModel
            {
                search = _searchData.GetSearch(searchId)
            };

            var criterias = _searchData.GetFullCriteriasBySearchId(searchId);
            foreach (var criteria in criterias)
            {
                var cti = new CriteriaItemList(criteria);
                criteriaListViewModel.criteriaItems.Add(cti);
            }
            return View(criteriaListViewModel);
        }

        public IActionResult Executions(int? searchId)
        {
            var executionsListViewModel = new ExecutionsListViewModel
            {
                search = _searchData.GetSearch(searchId),
                ExecutionsList = _searchData.GetFullExecutionsBySearchId(searchId)
            };
            return View(executionsListViewModel);
        }



        public async System.Threading.Tasks.Task<IActionResult> HomeAddSearch()
        {
            var newSearchVM = new NewSearchViewModel
            {
                user = await _userManager.GetUserAsync(HttpContext.User)
            };
            return View(newSearchVM);
        }
        
    }
}
