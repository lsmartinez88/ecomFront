using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomFront.Models;
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
        public SearchController(UserManager<ApplicationUser> userManager, ILogger<SearchController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Searches()
        {
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
