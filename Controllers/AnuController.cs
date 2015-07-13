using App.Models;
using Scaffold;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace App.Controllers
{
    public class AnuController : CRUDController<Anu, long>
    {
        public AnuController(DB dbContext): base(dbContext) {}

        public int PostCount([FromBody] Anu anu)
        {
            return anu.Dua;
        }
    }
}
