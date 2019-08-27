using System.Web;
using System.Web.Optimization;

namespace KioskoTCC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Assets/lib/js").Include(
                       "~/Assets/lib/js/jquery/jquery.js",
                       "~/Assets/lib/js/bootstrap/bootstrap.js"
                     ));

            bundles.Add(new ScriptBundle("~/Assets/kiosko/js/modules")
                        .IncludeDirectory("~/Assets/kiosko/js/modules/", "*.js", true));

            bundles.Add(new ScriptBundle("~/Assets/kiosko/js").Include(
                       "~/Assets/kiosko/js/kiosko.js"
                     ));


            bundles.Add(new StyleBundle("~/Assets/lib/css").Include(
                          "~/Assets/lib/css/bootstrap/bootstrap.css"
                          ));
            bundles.Add(new StyleBundle("~/Assets/kiosko/css").Include(
                         "~/Assets/kiosko/css/kiosko.css"
                         ));
        }
    }
}
