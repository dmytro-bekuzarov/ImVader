using System.Web.Optimization;

namespace ImVaderWebsite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/scripts/main").Include(
                "~/External/jquery/jquery-migrate-1.2.1.min.js",
                "~/External/bootstrap/js/bootstrap.min.js",
                "~/External/gozha-nav/jquery.mobile.menu.js",
                "~/External/modernizr/modernizr.custom.91224.js",
                "~/External/colorbox/jquery.colorbox.js",
                "~/External/jquery-cookie/jquery.cookie.js",
                "~/External/custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts/index").Include(
                "~/External/jquery-event/jquery.event.drag-2.2.js",
                "~/External/jquery-easing/jquery.easing.1.3.js",
                "~/External/jquery-easing/jquery.easing.1.3.js",
                "~/External/jquery-roundabout/jquery.roundabout.min.js",
                "~/External/swiper/idangerous.swiper.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts/demo").Include(
               "~/External/vkConnection.js",
               "~/External/graph.js",
               "~/External/spinner.js",
               "~/External/util.js",
               "~/External/toolTips.js"));
        }
    }
}
