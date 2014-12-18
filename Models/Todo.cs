using App.Controllers;
using Scaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Todo: IModel<long>
    {
        public long ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

}