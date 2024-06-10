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


            ///////////////////////
            ///////HOME PAGE///////
            ///////////////////////
            ///
            ///    <!-- Vendor CSS -->
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/vendor/font-awesome.min.css").Include("~/Content/assets/css/vendor/font-awesome.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/vendor/ionicons.css").Include("~/Content/assets/css/vendor/ionicons.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/vendor/simple-line-icons.css").Include("~/Content/assets/css/vendor/simple-line-icons.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/vendor/jquery-ui.min.css").Include("~/Content/assets/css/vendor/jquery-ui.min.css", new CssRewriteUrlTransform()));

            ///    <!-- Plugin CSS -->

            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/swiper-bundle.min.css").Include("~/Content/assets/css/plugins/swiper-bundle.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/animate.min.css").Include("~/Content/assets/css/plugins/animate.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/nice-select.css").Include("~/Content/assets/css/plugins/nice-select.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/venobox.min.css").Include("~/Content/assets/css/plugins/venobox.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/jquery.lineProgressbar.css").Include("~/Content/assets/css/plugins/jquery.lineProgressbar.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/homeBundle/assets/css/plugins/aos.min.css").Include("~/Content/assets/css/plugins/aos.min.css", new CssRewriteUrlTransform()));

            ///    <!-- Main CSS -->

            bundles.Add(new StyleBundle("~/homeBundle/assets/css/style.css").Include("~/Content/assets/css/style.css", new CssRewriteUrlTransform()));


            ///    <!-- ::::::::::::::All JS Files here :::::::::::::: -->
            ///    <!--Global Vendor, plugins JS -->


            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/modernizr-3.11.2.min.js").Include("~/Scripts/assets/js/vendor/modernizr-3.11.2.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/jquery-3.5.1.min.js").Include("~/Scripts/assets/js/vendor/jquery-3.5.1.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/jquery-migrate-3.3.0.min.js").Include("~/Scripts/lib/jquery-vectormap/jquery-jvectormap-world-mill-en.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/popper.min.js").Include("~/Scripts/assets/js/vendor/popper.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/bootstrap.min.js").Include("~/Scripts/assets/js/vendor/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/vendor/jquery-ui.min.js").Include("~/Scripts/assets/js/vendor/jquery-ui.min.js"));
            
            ///    <!--Plugins JS-->
            
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/swiper-bundle.min.js").Include("~/Scripts/assets/js/plugins/swiper-bundle.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/material-scrolltop.js").Include("~/Scripts/assets/js/plugins/material-scrolltop.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/jquery.nice-select.min.js").Include("~/Scripts/assets/js/plugins/jquery.nice-select.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/jquery.zoom.min.js").Include("~/Scripts/assets/js/plugins/jquery.zoom.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/venobox.min.js").Include("~/Scripts/assets/js/plugins/venobox.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/jquery.waypoints.js").Include("~/Scripts/assets/js/plugins/jquery.waypoints.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/jquery.lineProgressbar.js").Include("~/Scripts/assets/js/plugins/jquery.lineProgressbar.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/aos.min.js").Include("~/Scripts/assets/js/plugins/aos.min.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/jquery.instagramFeed.js").Include("~/Scripts/assets/js/plugins/jquery.instagramFeed.js"));
            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/plugins/ajax-mail.js").Include("~/Scripts/assets/js/plugins/ajax-mail.js"));

            ///    <!-- Main JS -->

            bundles.Add(new ScriptBundle("~/homeBundle/assets/js/main.js").Include("~/Scripts/assets/js/main.js"));
        }
    }
}