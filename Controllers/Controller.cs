using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Controllers
{
    public class Controller: System.Web.Mvc.Controller
    {
        protected DB db = new DB();
    }
}