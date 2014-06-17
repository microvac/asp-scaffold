using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using System.Data.Entity;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hello()
        {
            Todo todo = new Todo
            {
                Name="lalala",
            };
            db.Todos.Add(todo);
            db.SaveChanges();
            return new ContentResult
            {
                Content = "Count = "+db.Todos.Count(),
            };
        }
    }
}
