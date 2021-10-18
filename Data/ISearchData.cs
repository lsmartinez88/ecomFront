using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface ISearchData
    {
        List<Models.DbFirstModels.Search> GetFullSearches(string UserId);

        Models.DbFirstModels.Search GetSearch(int? searchId);
        Models.DbFirstModels.Search GetSearchWithExecutions(int? searchId);


        Execution GetExecution(int? executionId);

        List<Criterion> GetFullCriteriasBySearchId(int ?SearchId);
        Category GetCategoryById(string idCategory);

        List<Execution> GetFullExecutionsBySearchId(int? SearchId);

        List<Models.DbFirstModels.Search> GetFullExecutions(string UserId);

        Models.DbFirstModels.Search SaveSearch(Models.DbFirstModels.Search search);
        Criterion SaveCriteria(Criterion search);

        CriteriaAttribute SaveCriteriaAttribute(CriteriaAttribute criteriaAttribute);

        List<SalesQuantityMap> GetSalesMap(int ExecutionId, SalesType tipo);
        List<SalesQuantityMap> GetSalesMapCriteria(int ExecutionId, int CriteriaId, SalesType tipo);

        List<MainDashboard> GetMainDashboards(String userId);
        List<MainDashboard> GetSellersTypeSearch(int searchId);
        List<MainDashboard> GetAvgPriceSearch(int searchId);
        List<MainDashboard> GetAvgSellPriceSearch(int searchId);
        bool DeleteSearchById(int searchId);
        Models.DbFirstModels.Search CloneSearch(int searchId);
        bool NewExecution(int searchId);

    }
}
