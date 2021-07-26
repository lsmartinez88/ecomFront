using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ecomFront.Controllers
{
    
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
            var searches = _searchData.GetSearches(6);

            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return LocalRedirect("/Identity/Account/Login");
        }

        public IActionResult Criteria(int? searchId)
        {
            return View();
        }
    }
}
