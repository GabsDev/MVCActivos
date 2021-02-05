using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers

{
    public class DepreciacionController : Controller
    {
        // GET: Depreciacion
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IServiceActivo _ServiceActivo = new ServiceActivo();
                ViewBag.ListaActivos = _ServiceActivo.GetActivo();

                ViewModelParametroDepreciacion parametro = new ViewModelParametroDepreciacion();

                return View(parametro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

         [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxCalculoDepreciacion(string filtro)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            Activo miActivo = new Activo();
          
            CalculoDepreciacion miDepreciacion = new CalculoDepreciacion();
            miActivo= _ServiceActivo.GetActivoByID(filtro);          
            var listaDetalleDepreciacion = miDepreciacion.GetDetalleDepreciacion(miActivo);
  
            return PartialView("_DetalleDepreciacion", listaDetalleDepreciacion);
        }
    }
}