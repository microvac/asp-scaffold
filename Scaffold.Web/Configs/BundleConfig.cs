using System.Web;
using System.Web.Optimization;

namespace App.Configs
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use Bundle rather than StyleBundle or ScriptBundle in order to turn off
            // minification (takes the already minified files).

            // CSS Bundles

            bundles.Add(new StyleBundle("~/css/app")
                .Include("~/Content/app/app.css"));

            // Script Bundles

            bundles.Add(new ScriptBundle("~/js/libraries")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-classy.js"));

            bundles.Add(new ScriptBundle("~/js/app")
                .Include("~/Scripts/app/app.js")
                .Include("~/Scripts/app/controllers/*.js")
                .Include("~/Scripts/app/services/*.js")
                .Include("~/Scripts/app/directives/*.js"));
        }
    }
}