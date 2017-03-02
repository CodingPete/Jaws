using System.Web;
using System.Web.Optimization;

namespace Schichtplaner
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Template/Style").Include(
                "~/Content/template2/styles/datatables/dataTables.bootstrap4.min.css",
                "~/Content/template2/styles/datatables/responsive.bootstrap4.min.css",
                "~/Content/template2/styles/sweetalert/sweetalert.css",
                "~/Content/template2/styles/fullcalendar/fullcalendar.min.css",
                "~/Content/template2/styles/bootstrap/bootstrap.css",
                "~/Content/template2/styles/font-awesome/font-awesome.css",
                "~/Content/template2/styles/animate.css/animate.css",
                "~/Content/template2/styles/app.css",
                "~/Content/template2/styles/app.skins.css"
            ));

            bundles.Add(new ScriptBundle("~/Template/Scripts").Include(
                "~/Content/template2/scripts/modernizr/modernizr-2.6.2.js",
                "~/Content/template2/scripts/jquery/jquery.js",
                "~/Content/template2/scripts/tether/tether.js",
                "~/Content/template2/scripts/bootstrap/bootstrap.js",
                "~/Content/template2/scripts/bootstrap/respond.js",
                "~/Content/template2/scripts/fastclick/fastclick.js",

                "~/Content/template2/scripts/datatables/jquery.dataTables.min.js",
                "~/Content/template2/scripts/datatables/dataTables.bootstrap4.min.js",
                "~/Content/template2/scripts/datatables/dataTables.responsive.min.js",
                "~/Content/template2/scripts/datatables/responsive.bootstrap4.min.js",

                "~/Content/template2/scripts/sweetalert/sweetalert.min.js",

                "~/Content/template2/scripts/moment/moment.min.js",
                "~/Content/template2/scripts/jquery.ui/core.js",
                "~/Content/template2/scripts/jquery.ui/widget.js",
                "~/Content/template2/scripts/jquery.ui/mouse.js",
                "~/Content/template2/scripts/jquery.ui/draggable.js",
                "~/Content/template2/scripts/jqueryui-touch-punch/jquery.ui.touch-punch.js",
                "~/Content/template2/scripts/fullcalendar/fullcalendar.min.js",
                "~/Content/template2/scripts/fullcalendar/lang-all.js",
                "~/Content/template2/scripts/fullcalendar/gcal.js"
            ));

            bundles.Add(new ScriptBundle("~/Template/Main").Include(
                "~/Content/template2/scripts/main.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/template2/scripts/jquery/jquery.validate*"
            ));
        }
    }
}
