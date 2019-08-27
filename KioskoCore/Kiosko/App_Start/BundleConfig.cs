using System.Web;
using System.Web.Optimization;

namespace Kiosko
{
    public class BundleConfig
    {
       
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/Assets/js/lib").Include(
                      "~/Assets/js/lib/jquery-3.3.1.js",
                       "~/Assets/js/lib/popper.min.js",
                      "~/Assets/js/lib/bootstrap/bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/Assets/js/kiosko").Include(
                      "~/Assets/js/kiosko.js"
                     ));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                          "~/Assets/css/bootstrap/bootstrap.css",
                            "~/Assets/css/kiosko/kiosko.css"
                          ));
        }
    }
}
