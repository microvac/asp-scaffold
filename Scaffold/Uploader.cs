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

    public class Uploader
    {
        protected readonly string root;

        public Uploader() {
            root = HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            Directory.CreateDirectory(root);
        }

        public Task<UploadResult> PostFile<TBlob>(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            
            var provider = new GuidMultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).ContinueWith<UploadResult>(t =>
            {                
                if (t.IsFaulted || t.IsCanceled)
                {
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                }
                var files = provider.FileData.Select(i => {
                    var info = new FileInfo(i.LocalFileName);
                    var file = new FileResult
                    {
                        Root = root,
                        Name = "todo",
                        UploadID = info.Name,
                        Type = i.Headers.ContentType.MediaType,
                        Size = info.Length / 1024
                    };

                    return file;
                });

                var forms = provider.FormData.AllKeys.ToDictionary(k => k, k => provider.FormData[k]);

                return new UploadResult
                {
                    Files = files.ToList(),
                    Forms = forms,
                };
            });

            return task;
        }

    }

    public class UploadResult
    {
        public IList<FileResult> Files { get; set; }
        public IDictionary<String, String> Forms { get; set; }

        public String GetForm(String key)
        {
            if (!Forms.ContainsKey(key))
                return null;
            return Forms[key];
        }

        public void DeleteUnmoved()
        {
            foreach(var file in Files)
            {
                if (file.IsExists)
                    file.Delete();
            }
        }
    }

    public class GuidMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public GuidMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            return Guid.NewGuid().ToString("N"); // N = 32 digits no hyphens;
        }
    }

}
