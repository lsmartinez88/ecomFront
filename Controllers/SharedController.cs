using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomFront.Data;
using ecomFront.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecomFront.Controllers
{
    [Authorize]

    public class SharedController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ISharedData _sharedData;

        public SharedController(UserManager<ApplicationUser> userManager,
                                ISharedData sharedData)
        {
            _userManager = userManager;
            _sharedData = sharedData;
        }

        public async Task<JsonResult> GetActivity()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return Json(_sharedData.GetActivityInformation(user));
        }

        public async Task<JsonResult> CleanList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            _sharedData.CleanActivity(user);
            return Json(null);
        }
    }
}
