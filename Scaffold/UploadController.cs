using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;

namespace Scaffold
{   

    // SOURCE:
    // http://stackoverflow.com/questions/10320232/how-to-accept-a-file-post-asp-net-mvc-4-webapi
    // http://www.strathweb.com/2012/08/a-guide-to-asynchronous-file-uploads-in-asp-net-web-api-rtm/
    public class UploadController : ApiController
    {
        protected readonly string UploadFolder;

        public UploadController() : base() { }

        public UploadController(string uploadFolder) {
            UploadFolder = uploadFolder;
        }

        public Task<IEnumerable<FileDesc>> PostFile()
        {
            if(!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            
            string root = HttpContext.Current.Server.MapPath("~/Content/uploads/" + UploadFolder); 
            var provider = new CustomMultipartFormDataStreamProvider(root);             

            var task = Request.Content.ReadAsMultipartAsync(provider).ContinueWith<IEnumerable<FileDesc>>(t => {                
                if (t.IsFaulted || t.IsCanceled)
                {
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                }

                var result = provider.FileData.Select(i => {
                    var info = new FileInfo(i.LocalFileName);
                    return new FileDesc(
                        info.Name, 
                        i.Headers.ContentType.MediaType, 
                        root, 
                        info.Length / 1024);
                });

                BeforeResponse(result);
                return result;
            });

            return task;
        }

        protected virtual void BeforeResponse(IEnumerable<FileDesc> fileDescs) { }
    }

    public class FileDesc
    {
        public FileDesc(string name, string type, string path, long size)
        {
            Name = name;
            Type = type;
            Path = path;
            Size = size;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }

    public class GuidMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public GuidMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            return Guid.NewGuid().ToString("N"); // N = 32 digits no hyphens;
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Replace("\"", string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }

}
