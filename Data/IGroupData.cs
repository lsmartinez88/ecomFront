using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
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
        List<ListingIndicador> GetIndicador(int CriteriaId, int ExecutionId);
        List<ListingIndicador> GetIndicadorByExecution(int ExecutionId);
        List<PriceRangeGrouping> GetPriceRangeGrouping(int CriteriaId, int ExecutionId, String GroupingType);
        List<PriceRangeGrouping> GetPriceRangeGroupingByExecution(int ExecutionId, String GroupingType);
        List<AveragePricePerDay> GetAveragePriceByExecution(int ExecutionId);
        List<AveragePricePerDay> GetSalesQttyByExecution(int ExecutionId);
        List<WordCloudGrouping> GetWordCloudByExecution(int ExecutionId, int tipo);
        List<TrendsTreemap> GetTrendsTreemapByExecution(int ExecutionId);
        List<Event> GetEventsByRange(DateTime from, DateTime to);
        List<BarChartOportunity> GetBarChartOportunityByExecution(int ExecutionId);
        List<ListingIndicador> GetIndicadorFunnelByExecution(int ExecutionId);
        List<EventsIndicator> GetEventsIndicatorByExecution(int ExecutionId);

    }
}
