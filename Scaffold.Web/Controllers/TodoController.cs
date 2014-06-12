using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace App.Controllers
{
    public class TodoController : ApiController
    {
        private static IDictionary<long, Todo> todos = new Dictionary<long, Todo> 
        {  
            {1, new Todo{ID=1, Name="Satu"}},
            {2, new Todo{ID=2, Name="Dua"}},
            {3, new Todo{ID=3, Name="Tiga"}},
        };

        public IEnumerable<Todo> GetAll()
        {
            return todos.Values;
        }

        public Todo Get(long id)
        {
            return todos[id];
        }
    }
}
