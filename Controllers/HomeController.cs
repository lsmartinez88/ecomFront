using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ecomFront.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ecomFront.Services;
using ecomFront.Data;
using ecomFront.Models.MLModels;

namespace ecomFront.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IAuthData _authData;

        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger,
                                IAuthData authData)
        {
            _signInManager = signInManager;
            _logger = logger;
            _authData = authData;
        }

        public IActionResult Index()
        {
            /*try
            {
                List<CategoryML> resp = MLService.GetBaseCategories();
                foreach (CategoryML item in resp)
                {
                    CategoryML item2 = MLService.GetCategoryById(item.Id);
                }
            }
            catch (Exception e) { }*/


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
