using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class MultipartFile
    {
        public string Root { get; set; }
        public string UploadID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; } 

        public String FilePath
        {
            get
            {
                return Path.Combine(Root, UploadID);
            }
        }

        public void Move(String destination)
        {
            File.Move(FilePath, destination);
        }

        public void Delete()
        {
            File.Delete(FilePath);
        }

        public bool IsExists
        {
            get
            {
                return File.Exists(FilePath);
            }
        }
    }
}
