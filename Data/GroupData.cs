using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class GroupData : IGroupData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;

        public GroupData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }

        public List<ListingGrouping> GetGrouping(int CriteriaId, int ExecutionId, string GroupingType)
        {
            return _contextModel.ListingGrouping.Where(x => x.CriteriaId.Equals(CriteriaId)).Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).ToList();
        }

        public List<ListingGrouping> GetGroupingByExecution(int ExecutionId, string GroupingType)
        {
            return _contextModel.ListingGrouping.Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).GroupBy(x => new { x.ExecutionId, x.GroupingType, x.Clasification })
                    .Select(x => new ListingGrouping
                    {
                        ExecutionId = x.Key.ExecutionId,
                        GroupingType = x.Key.GroupingType,
                        Clasification = x.Key.Clasification,
                        Quantity = x.Sum(i => i.Quantity)
                    }).ToList();
        }
    }
}
