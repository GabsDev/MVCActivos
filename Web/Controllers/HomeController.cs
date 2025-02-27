﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Calificacion()
        {
            return View();
        }
    }
}