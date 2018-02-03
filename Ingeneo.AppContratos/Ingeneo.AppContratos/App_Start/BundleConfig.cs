using System.Web.Optimization;

namespace Ingeneo.AppContratos
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.12.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/Customer").Include(
                        "~/Scripts/CountriesAndCities.js",
                        "~/Scripts/CtrlDelCustomer.js"));

            //bundles.Add(new ScriptBundle("~/bundles/firebase").Include(
            //           "~/Scripts/firebase/app.js",
            //           "~/Scripts/firebase/sw.js"));

            bundles.Add(new ScriptBundle("~/bundles/pushN").Include(
                       "~/Scripts/push.min.js",
                       "~/Scripts/pushNotify.js"));



            bundles.Add(new ScriptBundle("~/bundles/Agreement").Include(
                             "~/Scripts/validateAgreement.js",
                             "~/Scripts/CrlDelAgreement.js"));

            bundles.Add(new ScriptBundle("~/bundles/PolicyCob").Include(
                           "~/Scripts/ValCobPolicy.js",
                           "~/Scripts/CtrlDeletCobert.js"));

            bundles.Add(new ScriptBundle("~/bundles/Policy").Include(
                           "~/Scripts/validatePolicy.js",
                           "~/Scripts/CtrlDelPolicy.js"));

            bundles.Add(new ScriptBundle("~/bundles/PolicyExt").Include(                          
                          "~/Scripts/validatePolicyExtension.js"));

            bundles.Add(new ScriptBundle("~/bundles/Extension").Include(
                          "~/Scripts/validateExtension.js",
                          "~/Scripts/CtrlDelExtension.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                       "~/Scripts/DataTables/jquery.dataTables.js",
                       "~/Scripts/DataTables/dataTables.buttons.min.js",
                       "~/Scripts/DataTables/buttons.flash.min.js",
                       "~/Scripts/DataTables/jszip.min.js",
                       "~/Scripts/DataTables/pdfmake.min.js",
                       "~/Scripts/DataTables/vfs_fonts.js",
                       "~/Scripts/DataTables/buttons.html5.js",
                       "~/Scripts/DataTables/buttons.print.js",
                       "~/Scripts/DataTables/dataTables.tableTools.js",
                       "~/Scripts/DataTables/dataTables.scroller.min.js",
                       "~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/Report").Include(
                           "~/Scripts/ReportDataTables.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/sweetalert.min.js",                      
                      "~/Scripts/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Tool").Include(
                         "~/Scripts/tooltip.js",
                         "~/Scripts/ValDates.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css",
                          "~/Content/bootstrap-datetimepicker.css",
                          "~/Content/sweetalert.css",                          
                          "~/Content/bootstrap-switch.min.css",
                          "~/Content/DataTables/css/buttons.dataTables.min.css",
                          "~/Content/DataTables/css/jquery.dataTables.min.css"));
        }
    }
}
