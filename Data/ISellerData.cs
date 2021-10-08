using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface ISellerData
    {
        List<Models.DbFirstModels.Search> GetFullSearches(string UserId);
        Models.DbFirstModels.Search GetSearch(int? searchId);
        Execution GetExecution(int? executionId);
        List<Criterion> GetFullCriteriasBySearchId(int? SearchId);
        List<TopSellers> GetSellersListByExecution(int? executionId);
        List<TopSellers> GetSalesMonthSeller(int? executionId, String sellerId);

        List<TopSellers> GetPriceMonthSeller(int? executionId, String sellerId);
        string GetPorcentajeDeVentas(int? executionId, String sellerId);
    }
}
