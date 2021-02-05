using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Administrador)]
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();
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


        // GetUsuarioByID
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult GetUsuarioByID(string pIdUsuario)
        {
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                Usuario miUsuario = _ServiceUsuario.GetUsuarioByID(pIdUsuario);

                // Configurar Serialización
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    },
                };


                string json = JsonConvert.SerializeObject(miUsuario, settings);

                return Content(json);
            }
            catch (Exception err)
            {
                ViewBag.Message = err.Message;
                return View();
            }
        }

        // GET: Usuario/Edit/admin
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(string id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            IServiceRol _ServiceRol = new ServiceRol();
            ViewBag.ListaRol = _ServiceRol.GetRol();
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }
                oUsuario = _ServiceUsuario.GetUsuarioByID(id);
                return View(oUsuario);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Usuario usuario, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (usuario.Foto == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        usuario.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }

                }

                // Es valido
                if (ModelState.IsValid)
                {
                    IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                    _ServiceUsuario.Save(usuario);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    IServiceRol _ServiceRol = new ServiceRol();
                    ViewBag.ListaRol = _ServiceRol.GetRol();
                    return View("Create", usuario);
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


        // GET: Rol/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            IServiceRol _ServiceRol = new ServiceRol();
            ViewBag.ListaRol = _ServiceRol.GetRol();
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(string id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                IServiceUsuario _ServiceUsuario = new ServiceUsuario();

                Usuario oUsuario = _ServiceUsuario.GetUsuarioByID(id);

                return View("Delete", oUsuario);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult DeleteUsuario(string id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceUsuario.DeleteUsuario(id);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();


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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(string id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                oUsuario = _ServiceUsuario.GetUsuarioByID(id);

                //return View(oUsuario);

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


        public ActionResult FilterName()
        {
            return View();
        }


        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult AjaxFilterByName(string filtro)
        {
            IEnumerable<Usuario> lista = null;
            IRepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();

            // Error porque viene en blanco 
            if (string.IsNullOrEmpty(filtro))
            {
                // Significa Error, va a ser capturado por onError del Javascript
                Response.StatusCode = -1;
                return View();
            }
            lista = _RepositoryUsuario.GetUsuarioByName(filtro);
            // Retorna un Partial View
            return PartialView("_PartialViewUsuario", lista);
        }


         /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult CreatePdfUsuarioCatalogo()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                // Extraer informacion
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Usuarios")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 6 columnas 
                Table table = new Table(6, true);

                 
                // los Encabezados
                table.AddHeaderCell("Login");
                table.AddHeaderCell("Descripción");
                table.AddHeaderCell("Fotografia");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Apellidos");
                table.AddHeaderCell("Estado");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Login.ToString()));
                    table.AddCell(new Paragraph(item.Rol.Descripcion.ToString()));
                     // Convierte la imagen que viene en Bytes en imagen para PDF
                    Image image = new Image(ImageDataFactory.Create(item.Foto));
                    // Tamaño de la imagen
                    image = image.SetHeight(75).SetWidth(75);
                    table.AddCell(image);
                    table.AddCell(new Paragraph(item.Nombre.ToString()));
                    table.AddCell(new Paragraph(item.Apellidos.ToString()));
                    table.AddCell(new Paragraph(item.Estado.ToString()));
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
