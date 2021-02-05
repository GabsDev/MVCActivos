using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MVCServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ActivoController : Controller
    {
        // GET: Activo
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("List");
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult List()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }


        // GetActivoByName
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult GetActivoByName(string name)
        {
            try
            {
                IServiceActivo _ServiceActivo = new ServiceActivo();
                var lista = _ServiceActivo.GetActivoByName(name).ToList();

                // Configurar Serialización
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    },
                };


                string json = JsonConvert.SerializeObject(lista, settings);

                return Content(json);
            }
            catch (Exception err)
            {
                ViewBag.Message = err.Message;
                return View();
            }
        }


        // GET: Activo/Edit/5
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Edit(string id)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            IServiceMarca _ServiceMarca = new ServiceMarca();
            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

            Activo oActivo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }
                ViewBag.ListaMarca = _ServiceMarca.GetMarca();
                ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();
                oActivo = _ServiceActivo.GetActivoByID(id);

                return View(oActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(Activo activo, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (activo.FotoActivo == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        activo.FotoActivo = target.ToArray();
                        activo.FotoFactura = target.ToArray();

                        ModelState.Remove("FotoActivo");
                        ModelState.Remove("FotoFactura");
                    }

                }

                // Es valido
                if (ModelState.IsValid)
                {
                    IServiceActivo _ServiceActivo = new ServiceActivo();
                    IServiceMarca _ServiceMarca = new ServiceMarca();
                    _ServiceMarca.GetMarcaByID(activo.IdMarca);



                    _ServiceActivo.Save(activo);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    IServiceMarca _ServiceMarca = new ServiceMarca();
                    IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

                    ViewBag.ListaMarca = _ServiceMarca.GetMarca();
                    ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();


                    return View("Create", activo);
                }

                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Activo/Create
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Create()
        {
            IServiceMarca _ServiceMarca = new ServiceMarca();
            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

            ViewBag.ListaMarca = _ServiceMarca.GetMarca();
            ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();

            ServiceBCCR oServiceBCCR = new ServiceBCCR();

            ViewBag.ListaPrecioDolar = oServiceBCCR.GetDolar("c");

            return View();
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Delete(string id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                IServiceActivo _ServiceActivo = new ServiceActivo();

                Activo oActivo = _ServiceActivo.GetActivoByID(id);

                return View("Delete", oActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult DeleteActivo(string id)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceActivo.DeleteActivo(id);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();

                    IServiceMarca _ServiceMarca = new ServiceMarca();
                    IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

                    ViewBag.ListaMarca = _ServiceMarca.GetMarca();
                    ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();
                    // return View("list");
                }

                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Details(string id)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            IServiceMarca _ServiceMarca = new ServiceMarca();
            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

            Activo oActivo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                oActivo = _ServiceActivo.GetActivoByID(id);



                ViewBag.ListaMarca = _ServiceMarca.GetMarca();
                ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();

                //return View(oActivo);

                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult CreatePdfActivoCatalogo()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                // Extraer informacion
                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Activos")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 6 columnas 
                Table table = new Table(6, true);


                // los Encabezados
                table.AddHeaderCell("N Serie");
                table.AddHeaderCell("Modelo");
                table.AddHeaderCell("Fecha Compra");
                table.AddHeaderCell("Costo");
                table.AddHeaderCell("Descrpcion");
                table.AddHeaderCell("FotoActivo");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.NumSerie.ToString()));
                    table.AddCell(new Paragraph(item.Modelo.ToString()));
                    table.AddCell(new Paragraph(item.FechCompra.ToString()));
                    table.AddCell(new Paragraph(item.Costo.ToString()));
                    table.AddCell(new Paragraph(item.Descripcion.ToString()));
                    // Convierte la imagen que viene en Bytes en imagen para PDF
                    Image image = new Image(ImageDataFactory.Create(item.FotoActivo));
                    // Tamaño de la imagen
                    image = image.SetHeight(75).SetWidth(75);
                    table.AddCell(image);
                }
                doc.Add(table);

                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }






    }
}