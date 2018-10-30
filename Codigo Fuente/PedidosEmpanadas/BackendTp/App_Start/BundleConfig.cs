using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BackendTp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery/jquery-3.3.1.min.js",
                        "~/Scripts/modernizr/modernizr-2.8.3.js",
                        "~/Scripts/boostrap/bootstrap.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/scriptsPropios").Include(
                        "~/Scripts/utilidades/utilidades.js",
                        "~/Scripts/vistas/home.js"
                        ));

            bundles.Add(new StyleBundle("~/css").Include(
                          "~/Content/bootstrap/bootstrap.min.css",
                          "~/Content/font-awesome.min.css",
                          "~/Content/Site.css"));

        }
    }
}