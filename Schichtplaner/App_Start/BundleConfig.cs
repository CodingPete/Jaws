using System.Web;
using System.Web.Optimization;

namespace Schichtplaner
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Template/Style").Include(
                        "~/Content/template2/styles/datatables/dataTables.bootstrap4.css",
                        "~/Content/template2/styles/sweetalert/sweetalert.css",
                        "~/Content/template2/styles/fullcalendar/fullcalendar.min.css",
                        "~/Content/template2/styles/bootstrap/bootstrap.css",
                        "~/Content/template2/styles/font-awesome/font-awesome.css",
                        "~/Content/template2/styles/animate.css/animate.css",
                        "~/Content/template2/styles/app.css",
                        "~/Content/template2/styles/app.skins.css"
                ));

            bundles.Add(new ScriptBundle("~/Template/Scripts").Include(
                "~/Content/template2/scripts/jquery/jquery.js",
                "~/Content/template2/scripts/tether/tether.js",
                "~/Content/template2/scripts/bootstrap/bootstrap.js",
                "~/Content/template2/scripts/fastclick/fastclick.js",
                "~/Content/template2/scripts/constants.js",
                "~/Content/template2/scripts/main.js",

                "~/Content/template2/scripts/dashboard.js",
                "~/Content/template2/scripts/datatables/jquery.dataTables.js",
                "~/Content/template2/scripts/datatables/dataTables.bootstrap4.js",
                "~/Content/template2/scripts/sweetalert/sweetalert.min.js",

                "~/Content/template2/scripts/moment/moment.min.js",
                "~/Content/template2/scripts/jquery.ui/core.js",
                "~/Content/template2/scripts/jquery.ui/widget.js",
                "~/Content/template2/scripts/jquery.ui/mouse.js",
                "~/Content/template2/scripts/jquery.ui/draggable.js",
                "~/Content/template2/scripts/jqueryui-touch-punch/jquery.ui.touch-punch.js",
                "~/Content/template2/scripts/fullcalendar/fullcalendar.min.js",
                "~/Content/template2/scripts/fullcalendar/gcal.js",

                "~/Content/template2/scripts/calendar.js"

            ));
        }
    }
}
