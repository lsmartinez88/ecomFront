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
            return _contextModel.ListingGrouping.Include(li => li.ItemGrouping).Where(x => x.CriteriaId.Equals(CriteriaId)).Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).ToList();
        }

        public List<ListingGrouping> GetGroupingByExecution(int ExecutionId, string GroupingType)
        {
            List<ListingGrouping> preLista = _contextModel.ListingGrouping.Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).GroupBy(x => new { x.ExecutionId, x.GroupingType, x.ItemGroupingId })
                    .Select(x => new ListingGrouping
                    {
                        ExecutionId = x.Key.ExecutionId,
                        GroupingType = x.Key.GroupingType,
                        ItemGroupingId = x.Key.ItemGroupingId,
                        Quantity = x.Sum(i => i.Quantity)
                    }).ToList();

            preLista.ForEach(li => { li.ItemGrouping = _contextModel.ItemGrouping.Find(li.ItemGroupingId); });

            return preLista;
        }

        public List<ListingIndicador> GetIndicador(int CriteriaId, int ExecutionId)
        {
            return _contextModel.ListingIndicador.Where(li => li.CriteriaId.Equals(CriteriaId)).Where(li => li.ExecutionId.Equals(ExecutionId)).ToList();
        }

        public List<ListingIndicador> GetIndicadorByExecution(int ExecutionId)
        {
            return _contextModel.ListingIndicador.Where(x => x.ExecutionId.Equals(ExecutionId)).GroupBy(x => new { x.ExecutionId, x.TipoIndicador })
                    .Select(x => new ListingIndicador
                    {
                        ExecutionId = x.Key.ExecutionId,
                        TipoIndicador = x.Key.TipoIndicador,
                        Valor = x.Sum(i => i.Valor)
                    }).ToList();
        }
    }
}
