using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelDetalleDepreciacion
    {
        public string Activo { get; set; }
        public Nullable<decimal> DepreciacionMensual { get; set; }
        public Nullable<decimal> ValorActual { get; set; }
        public Nullable<decimal> ValorFinal { get; set; }
        public Nullable<int> NumeroMes { get; set; }
    }
}