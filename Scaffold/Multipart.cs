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
using Scaffold.Utils;
using System.Web.Http.ValueProviders;
using System.Globalization;
using System.Web.Http.Metadata.Providers;

namespace Scaffold
{   
    public class MultipartModelBinder: IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType, actionContext);
            return true;
        }
    }


    [ModelBinder(typeof(MultipartModelBinder))]
    public class Multipart
    {
        protected readonly string root;
        private HttpActionContext actionContext;

        public IList<MultipartFile> Files { get; set; }
        public IDictionary<String, String> Forms { get; set; }

        public Multipart(HttpActionContext actionContext)
        {
            this.actionContext = actionContext;
            root = HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            Directory.CreateDirectory(root);
            AsyncHelper.RunSync(ProcessBody);

        }

        private Task ProcessBody()
        {
            var request = actionContext.Request;

            if (!request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            
            var provider = new GuidMultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
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
                    var file = new MultipartFile
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

                this.Files = files.ToList();
                this.Forms = forms;
            });

            return task;
        }

        public String GetForm(String key)
        {
            if (!Forms.ContainsKey(key))
                return null;
            return Forms[key];
        }

        public void DeleteUnmoved()
        {
            foreach (var file in Files)
            {
                if (file.IsExists)
                    file.Delete();
            }
        }

    }

    [ModelBinder(typeof(MultipartModelBinder))]
    public class Multipart<T>: Multipart
    {
        public T Entity { get; set; }
        public Multipart(HttpActionContext actionContext)
            : base(actionContext)
        {
            var valueProvider = new SimpleHttpValueProvider();
            foreach (var key in Forms.Keys)
                valueProvider[key] = Forms[key];

            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(null, typeof(T)),
                ValueProvider = valueProvider
            };
            bool retVal = actionContext.Bind(bindingContext);
            if (retVal)
                Entity = (T) bindingContext.Model;
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

    public class SimpleHttpValueProvider : Dictionary<string, object>, IValueProvider
    {
        private readonly CultureInfo _culture;

        public SimpleHttpValueProvider()
            : this(null)
        {
        }

        public SimpleHttpValueProvider(CultureInfo culture)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            _culture = culture ?? CultureInfo.InvariantCulture;
        }

        // copied from ValueProviderUtil
        public bool ContainsPrefix(string prefix)
        {
            foreach (string key in Keys)
            {
                if (key != null)
                {
                    if (prefix.Length == 0)
                    {
                        return true; // shortcut - non-null key matches empty prefix
                    }

                    if (key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        if (key.Length == prefix.Length)
                        {
                            return true; // exact match
                        }
                        else
                        {
                            switch (key[prefix.Length])
                            {
                                case '.': // known separator characters
                                case '[':
                                    return true;
                            }
                        }
                    }
                }
            }

            return false; // nothing found
        }

        public ValueProviderResult GetValue(string key)
        {
            object rawValue;
            if (TryGetValue(key, out rawValue))
            {
                return new ValueProviderResult(rawValue, Convert.ToString(rawValue, _culture), _culture);
            }
            else
            {
                // value not found
                return null;
            }
        }
    }
}
