using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface IGroupData
    {
        List<ListingGrouping> GetGrouping(int CriteriaId, int ExecutionId, String GroupingType);
        List<ListingGrouping> GetGroupingByExecution(int ExecutionId, String GroupingType);

    }
}
