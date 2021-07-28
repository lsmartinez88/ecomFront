using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ecomFront.Models;
using Microsoft.AspNetCore.Identity;
using ecomFront.Services;
using ecomFront.Data;

namespace ecomFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ISearchData _searchData;
        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger,
                                ISearchData searchData)
        {
            _signInManager = signInManager;
            _logger = logger;
            _searchData = searchData;
        }

        public IActionResult Index()
        {
            try
            {
                var resp = _searchData.GetSearches(3);
            }
            catch (Exception e) { }
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return LocalRedirect("/Identity/Account/Login");
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
                return LocalRedirect("/Identity/Account/Logout");
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
