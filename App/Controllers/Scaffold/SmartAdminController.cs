﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using System.Data.Entity;

namespace App.Controllers.Scaffold
{
    public class SmartAdminController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Scaffold/Smartadmin/Index.cshtml");
        }

        public ActionResult Partials(String fileName)
        {
            return PartialView(String.Format("~/Views/Scaffold/Smartadmin/Partials/{0}.cshtml", fileName));
        }
    }
}
