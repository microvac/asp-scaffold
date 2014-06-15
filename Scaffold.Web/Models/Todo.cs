using App.Controllers;
using Scaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    [ToTS(TSFlag.JSonData)]
    public class Todo: Model
    {
        public String Name { get; set; }
        public String Description { get; set; }
    }

}