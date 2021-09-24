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
        List<Search> GetFullSearches(string UserId);
        Search GetSearch(int? searchId);
        Execution GetExecution(int? executionId);
        List<Criterion> GetFullCriteriasBySearchId(int? SearchId);
        List<TopSellers> GetSellersListByExecution(int? executionId);
    }
}
