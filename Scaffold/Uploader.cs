using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;

namespace Scaffold
{   
    public class UploaderModelBinder: IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            bindingContext.Model = new Uploader(actionContext.Request);
            return true;
        }
    }

    [ModelBinder(typeof(UploaderModelBinder))]
    public class Uploader
    {
        protected readonly string root;
        private HttpRequestMessage request;

        public Uploader(HttpRequestMessage request) {
            this.request = request;
            root = HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            Directory.CreateDirectory(root);
        }

        public Task<UploadResult> PostFile()
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
                    String fileName = null;
                    if (i.Headers.ContentDisposition != null)
                        fileName = i.Headers.ContentDisposition.FileName;
                    if (fileName != null)
                        fileName = Path.GetFileName(fileName.Replace("\"", ""));
                    var file = new FileResult
                    {
                        Root = root,
                        Name = fileName,
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
