using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public interface IBlob
    {
        string UploadID { get; set; }
        String UploadFolder { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        long Size { get; set; } 
    }
}
