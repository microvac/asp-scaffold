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
    public class TodoController : CRUDController<Todo, long>
    {
        public TodoController(DB dbContext): base(dbContext) {}

        public int GetAduh() 
        {
            return 0;
        }

        public int PostBody([FromBody] Todo todo) 
        {
            return 0;
        }

    }
}
