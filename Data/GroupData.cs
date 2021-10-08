using ecomFront.Common;
using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
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

        public List<PriceRangeGrouping> GetPriceRangeGroupingByExecution(int ExecutionId, string GroupingType)
        {
            List<PriceRangeGrouping> preLista = _contextModel.PriceRangeGrouping.Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).GroupBy(x => new { x.ExecutionId, x.GroupingType, x.ItemGroupingId,x.RangoDesde,x.RangoHasta })
                   .Select(x => new PriceRangeGrouping
                   {
                       ExecutionId = x.Key.ExecutionId,
                       GroupingType = x.Key.GroupingType,
                       ItemGroupingId = x.Key.ItemGroupingId,
                       RangoDesde = x.Key.RangoDesde,
                       RangoHasta = x.Key.RangoHasta,
                       CantidadPublicaciones= x.Sum(i => i.CantidadPublicaciones),
                       CantidadVentas = x.Sum(i => i.CantidadVentas)
                   }).OrderBy(pi => pi.RangoDesde).ThenBy(pi => pi.ItemGroupingId).ToList();

            preLista.ForEach(li => { li.ItemGrouping = _contextModel.ItemGrouping.Find(li.ItemGroupingId); });

            return preLista;
        }

        public List<PriceRangeGrouping> GetPriceRangeGrouping(int CriteriaId, int ExecutionId, string GroupingType)
        {
            return _contextModel.PriceRangeGrouping.Include(li => li.ItemGrouping).Where(x => x.CriteriaId.Equals(CriteriaId)).Where(x => x.ExecutionId.Equals(ExecutionId)).Where(x => x.GroupingType.Equals(GroupingType)).OrderBy(pi => pi.RangoDesde).ThenBy(pi => pi.ItemGroupingId).ToList();
        }

        public List<AveragePricePerDay> GetAveragePriceByExecution(int ExecutionId)
        {
            return _contextModel.AveragePricePerDay.Where(x => x.ExecutionId.Equals(ExecutionId) && x.PrecioMedio > 0).OrderBy(pi => pi.Fecha).ToList();
        }

        public List<AveragePricePerDay> GetSalesQttyByExecution(int ExecutionId)
        {
            return _contextModel.AveragePricePerDay.Where(x => x.ExecutionId.Equals(ExecutionId)).GroupBy(x => new { x.ExecutionId, x.Fecha })
                .Select(x => new AveragePricePerDay
                {
                    ExecutionId = x.Key.ExecutionId,
                    Fecha = x.Key.Fecha,
                    CantidadVentas = x.Sum(i => i.CantidadVentas)
                }).OrderBy(pi => pi.Fecha).ToList();
        }

        public List<WordCloudGrouping> GetWordCloudByExecution(int ExecutionId, int tipo)
        {
                return _contextModel.WordCloudGrouping.Where(x => x.ExecutionId.Equals(ExecutionId)).GroupBy(x => new { x.ExecutionId, x.Palabra })
                    .Select(x => new WordCloudGrouping
                    {
                        ExecutionId = x.Key.ExecutionId,
                        Palabra = x.Key.Palabra,
                        CantidadPublicaciones = x.Sum(i => i.CantidadPublicaciones),
                        CantidadVentas = x.Sum(i => i.CantidadVentas),
                        IndicadorOportunidad = x.Sum(i => i.IndicadorOportunidad)

                    }
                 ).ToList();
        }

        public List<TrendsTreemap> GetTrendsTreemapByExecution(int ExecutionId)
        {
            return _contextModel.TrendsTreemap.Where(x => x.ExecutionId.Equals(ExecutionId)).ToList();
        }

        public List<BarChartOportunity> GetBarChartOportunityByExecution(int ExecutionId)
        {
            return _contextModel.BarChartOportunity.Where(x => x.ExecutionId.Equals(ExecutionId)).OrderByDescending(a => a.IndicadorOportunidad).Take(10).ToList();
        }

        public List<Event> GetEventsByRange(DateTime from, DateTime to)
        {
            return _contextDbFirst.Events.Where(e => e.FechaDesde >= from && e.FechaDesde <= to && e.Estado == 1).ToList();
        }

        public List<ListingIndicador> GetIndicadorFunnelByExecution(int ExecutionId)
        {
            return _contextModel.ListingIndicador.Where(x => x.ExecutionId.Equals(ExecutionId) && !x.TipoIndicador.Equals("PUBLICACIONES") && !x.TipoIndicador.Equals("REVIEWS")).GroupBy(x => new { x.ExecutionId, x.TipoIndicador })
                    .Select(x => new ListingIndicador
                    {
                        ExecutionId = x.Key.ExecutionId,
                        TipoIndicador = x.Key.TipoIndicador,
                        Valor = x.Sum(i => i.Valor)
                    }).ToList();
        }

        public List<EventsIndicator> GetEventsIndicatorByExecution(int ExecutionId)
        {
            var eventos = _contextDbFirst.Events.Where(e => e.FechaDesde >= DateTime.Now.AddYears(-1) && e.FechaDesde <= DateTime.Now && e.Estado == 1).ToList();
            List<AveragePricePerDay> salesItems = GetSalesQttyByExecution(ExecutionId);
            var _return = new List<EventsIndicator>();
            var promedioGeneral = salesItems.Average(s => s.CantidadVentas);

            eventos.ForEach(ev =>
            {
                var _event = new EventsIndicator
                {
                    average = (int)salesItems.Where(e => e.Fecha >= ev.FechaDesdeVenta && e.Fecha <= ev.FechaHastaVenta).Average(s => s.CantidadVentas),
                    name = ev.Name,
                    fecha = (DateTime)ev.FechaDesde
                };
                _event.difference = _event.average - promedioGeneral;
                _return.Add(_event);
            });

            return _return.OrderByDescending(e => e.difference).Take(10).ToList();
        }
    }
}
