using System.Web;
using System.Web.Optimization;

namespace AGFotografia
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/otros").Include( //parte del template
                      /*"~/Scripts/custom.script.js"*/
                      "~/Scripts/jquery.gridrotator.js",
                      "~/Scripts/modernizr.custom.js",
                      "~/Scripts/baron.js",
                      //"~/Scripts/custom.script.js",
                      //light gallery agregada
                      "~/Scripts/lightgallery.js",
                      "~/Scripts/lg-autoplay.js",
                      "~/Scripts/lg-fullscreen.js",
                      "~/Scripts/lg-thumbnail.js",
                      "~/Scripts/jquery.flexslider-min.js",
                      //"~/Scripts/jquery.flexslider-min2.js",
                      //hasta aca
                      "~/Scripts/site.js",
                      "~/Scripts/jquery.countTo.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                    "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/landing").Include(
                     "~/Content/landing.css"));

            bundles.Add(new StyleBundle("~/Content/otros").Include( //parte del template
                "~/Content/animate.css",
                "~/Content/css.css", //cambie el nombre antes era solo "CSS", se agrego la extencion .css
                "~/Content/flexslider.css",
                "~/Content/font-awesome.min.css",
                "~/Content/lightgallery.css",
                "~/Content/light.background.css",
                 "~/Content/hover.css",
                "~/Content/style.css"));

            bundles.Add(new StyleBundle( "~/Theme/blue").Include( "~/Content/blue.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/green").Include("~/Content/green.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/lightgreen").Include("~/Content/lightgreen.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/lightred").Include("~/Content/lightred.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/pink").Include("~/Content/pink.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/purple").Include("~/Content/purple.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/red").Include("~/Content/red.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/turquoise").Include("~/Content/turquoise.color.css") ); //Themes
            bundles.Add(new StyleBundle("~/Theme/yellow").Include("~/Content/yellow.color.css") ); //Themes

            bundles.Add(new StyleBundle("~/Theme/switch.color").Include("~/Content/switch.color.css") ); //Themes

            bundles.Add(new StyleBundle("~/Background/light.background").Include( "~/Content/blue.color.css") ); //Background


        }
    }
}
