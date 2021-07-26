using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Event
    {
        public long IdEvent { get; set; }
        public long Version { get; set; }
        public ulong? Estado { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Name { get; set; }
    }
}
