﻿using System.Web;
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

            bundles.Add(new StyleBundle("~/css/smartadmin")
                .Include("~/Scaffold/Content/smartadmin/smartadmin-production.css")
                .Include("~/Scaffold/Content/smartadmin/smartadmin-skins.css"));

            // Script Bundles

            bundles.Add(new ScriptBundle("~/js/libraries")
                .Include("~/Scaffold/Scripts/jquery-{version}.js")
                .Include("~/Scaffold/Scripts/bootstrap.js")
                .Include("~/Scaffold/Scripts/angular.js")
                .Include("~/Scaffold/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/js/scaffold")
                .Include("~/Scaffold/Scripts/Scaffold/Scaffold.js"));

            bundles.Add(new ScriptBundle("~/js/scaffold/flot")
                .Include("~/Scaffold/Scripts/flot/jquery.flot.js")
                .Include("~/Scaffold/Scripts/Scaffold/Directives/flot.js"));

            bundles.Add(new ScriptBundle("~/js/scaffold/sparkline")
                .Include("~/Scripts/Scaffold/Lib/jquery.sparkline.js")
                .Include("~/Scripts/Scaffold/Directives/sparkline.js"));

            bundles.Add(new ScriptBundle("~/js/app")
                .Include("~/Scripts/Scaffold/Models.js")
                .Include("~/Scripts/App/App.js")
                .Include("~/Scripts/App/Controllers/*.js")
                .Include("~/Scripts/App/Directives/*.js"));

            bundles.Add(new ScriptBundle("~/js/scaffold/smartadmin")
                .Include("~/Scaffold/Scripts/Scaffold/Demo/Smartadmin/*.js"));
        }
    }
}