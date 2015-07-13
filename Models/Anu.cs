using Scaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Anu: IModel<long>
    {
        public long ID { get; set; }
        public int Dua { get; set; }
    }
}