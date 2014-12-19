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
    public class Uploader
    {
        public String UploadFolder { get; private set; }

        protected readonly string root;

        public Uploader(string uploadFolder) {
            UploadFolder = uploadFolder;
            root = HttpContext.Current.Server.MapPath("~/Content/uploads/" + UploadFolder);
            Directory.CreateDirectory(root);
        }

        public Task<UploadResult<TBlob>> PostFile<TBlob>(HttpRequestMessage request)
            where TBlob: IBlob, new()
        {
            if (!request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            
            var provider = new GuidMultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).ContinueWith<UploadResult<TBlob>>(t =>
            {                
                if (t.IsFaulted || t.IsCanceled)
                {
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                }
                var blobs = provider.FileData.Select(i => {
                    var info = new FileInfo(i.LocalFileName);
                    var blob = new TBlob
                    {
                        Name = "todo",
                        UploadID = info.Name,
                        Type = i.Headers.ContentType.MediaType,
                        Size = info.Length / 1024,
                        UploadFolder = UploadFolder
                    };

                    return blob;
                });

                var forms = provider.FormData.AllKeys.ToDictionary(k => k, k => provider.FormData[k]);

                return new UploadResult<TBlob>
                {
                    Files = blobs,
                    Forms = forms,
                };
            });

            return task;
        }

        public static String ToAbsolutePath(IBlob blob)
        {
            var root = HttpContext.Current.Server.MapPath("~/Content/uploads/" + blob.UploadFolder);
            return Path.Combine(root, blob.UploadID);
        }

    }

    public class UploadResult<TBlob>
        where TBlob: IBlob, new()
    {
        public IEnumerable<TBlob> Files { get; set; }
        public IDictionary<String, String> Forms { get; set; }
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
