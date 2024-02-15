using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SmartStore.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/js/vendor.min.js").Include("~/Scripts/js/vendor.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib/apexcharts/apexcharts.min.js").Include("~/Scripts/libs/apexcharts/apexcharts.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib/jquery-sparkline/jquery.sparkline.min.js").Include("~/Scripts/lib/jquery-sparkline/jquery.sparkline.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib/jquery-vectormap/jquery-jvectormap-1.2.2.min.js").Include("~/Scripts/lib/jquery-vectormap/jquery-jvectormap-1.2.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib/jquery-vectormap/jquery-jvectormap-world-mill-en.js").Include("~/Scripts/lib/jquery-vectormap/jquery-jvectormap-world-mill-en.js"));


            bundles.Add(new ScriptBundle("~/bundles/libs/peity/jquery.peity.min.js").Include("~/Scripts/libs/peity/jquery.peity.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/pages/dashboard-2.init.js").Include("~/Scripts/js/pages/dashboard-2.init.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/app.min.js").Include("~/Scripts/js/app.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/css/icons.min.css").Include("~/Content/css/icons.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/css/app.min.css").Include("~/Content/css/app.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/vectormap/jquery-jvectormap.css").Include("~/Content/css/jquery-jvectormap.css", new CssRewriteUrlTransform()));


            ///////////////////////
            ///REGISTRATION PAGE///
            ///////////////////////

            bundles.Add(new StyleBundle("~/regBundle/css/styleRegistration.css").Include("~/Content/css/styleRegistration.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/regBundle/js/mainRegistration.js").Include("~/Scripts/js/mainRegistration.js"));
            bundles.Add(new ScriptBundle("~/regBundle/lib/jquery/jquery.min.js").Include("~/Scripts/lib/jquery/jquery.min.js"));
            //mainRegistration.js
            //jquery/jquery.min.js


        }
    }
}