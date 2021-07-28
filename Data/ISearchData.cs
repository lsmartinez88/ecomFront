using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface ISearchData
    {
        List<Search> GetFullSearches(int UserId);
        List<Criterion> GetFullCriteriasBySearchId(int ?SearchId);
        Category GetCategoryById(string idCategory);

    }
}
