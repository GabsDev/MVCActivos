using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModel;

namespace Web.Models
{
    public class CalculoDepreciacion
    {

          public decimal Monto { set; get; }

          public List<ViewModelDetalleDepreciacion> GetDetalleDepreciacion(Activo parametro)
        {
            List<ViewModelDetalleDepreciacion> lista = new List<ViewModelDetalleDepreciacion>();
            ViewModelDetalleDepreciacion oViewModelDetalleDepreciacion = null;
            decimal monto = parametro.ValorActual;
            decimal depreciacion = monto / 60;


            for (int i = 1; i <= 60; i++)
            {
                oViewModelDetalleDepreciacion = new ViewModelDetalleDepreciacion();
                oViewModelDetalleDepreciacion.NumeroMes = i;
                oViewModelDetalleDepreciacion.ValorActual = monto;
                oViewModelDetalleDepreciacion.DepreciacionMensual = depreciacion;
                oViewModelDetalleDepreciacion.ValorFinal = monto - oViewModelDetalleDepreciacion.DepreciacionMensual;
                monto =(decimal) oViewModelDetalleDepreciacion.ValorFinal;
                lista.Add(oViewModelDetalleDepreciacion);

            }
            return lista;
        }

    }
}