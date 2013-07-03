namespace Web.Infrastructure
{
    using System.Web.Optimization;
    using MvcExtensions;

    public class RegisterBundles : BootstrapperTask
    {
        public override TaskContinuation Execute()
        {
            BundleTable.EnableOptimizations = true;

            BundleTable.Bundles.Add(new StyleBundle("~/CommonStyles")
                {
                    Transforms =
                        {
                            new LessTransform(),
                        }
                }.Include("~/Content/themes/base/jquery-ui.css",
                          "~/Content/bootstrap.min.css",
                          "~/Content/bootstrap-responsive.min.css",
                          "~/Content/site.less"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Modernizr")
                .Include("~/Scripts/modernizr-{version}.js"));


            BundleTable.Bundles.Add(new ScriptBundle("~/CommonScripts")
                .Include("~/Scripts/jquery-{version}.min.js",
                            "~/Scripts/jquery-ui-{version}.min.js",
                            "~/Scripts/jquery.validate.min.js",
                            "~/Scripts/jquery.validate.unobtrusive.min.js",
                            "~/Scripts/jquery.unobtrusive-ajax.min.js",
                            "~/Scripts/bootstrap.min.js"));

            BundleTable.Bundles.UseCdn = true;

            return TaskContinuation.Continue;
        }
    }
}