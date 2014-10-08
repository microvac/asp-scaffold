using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public interface IModel<TId>
    {
        TId ID { get; set; }
    }
}
