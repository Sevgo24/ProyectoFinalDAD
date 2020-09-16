using System.Web;
using System.Web.Optimization;

namespace DAD.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/placeholder").Include(
                      "~/Scripts/placeholder-shim.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Scripts/custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                      "~/Scripts/jquery.dataTables.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablebootstrap").Include(
                      "~/Scripts/dataTables.bootstrap4.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                     

                      "~/Content/dataTables.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/Content/LoginCss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/login.css",
                      "~/Content/animate-custom.css",
                      "~/Content/bootstrap3.css",
                      "~/Content/toastr.css"

                      ));
        }
    }
}
