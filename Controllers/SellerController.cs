using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SalesViewModels;
using ecomFront.Models.SellerViewModel;
using ecomFront.Models.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ecomFront.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private ISellerData _sellerData;

        public SellerController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                ISellerData sellerData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
            _sellerData = sellerData;
        }

        public IActionResult HomeSellers(int? executionId)
        {
            var homeSellerViewModel = new HomeSellerViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            homeSellerViewModel.Search = _searchData.GetSearch((int)homeSellerViewModel.Execution.SearchId);
            homeSellerViewModel.Criteria = _searchData.GetFullCriteriasBySearchId((int)homeSellerViewModel.Search.IdSearch);

            var topSellers = _sellerData.GetSellersListByExecution(executionId);
            int index = 0;
            foreach(var sellerId in topSellers.Select(ts => ts.SellerId).Distinct())
            {
                var sellerInformation = new SellerInformation
                {
                    sellerId = sellerId,
                    style = Colores.GetStyle(index),
                    SellerData = topSellers.Where(ts => ts.SellerId == sellerId).ToList()
                };

                index++;
                homeSellerViewModel.SellerInformation.Add(sellerInformation);
            }

            return View(homeSellerViewModel);
        }

    }
}
