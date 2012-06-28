namespace Web.Infrastructure
{
    using System.Web.Optimization;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;
    using BundleTransformer.Core.Translators;
    using BundleTransformer.Less.Translators;
    using BundleTransformer.Yui.Minifiers;
    using MvcExtensions;

    public class RegisterBundles : BootstrapperTask
    {
        public override TaskContinuation Execute()
        {
            var cssTransformer = new CssTransformer(new YuiCssMinifier(), new ITranslator[] {new LessTranslator()});
            var jsTransformer = new JsTransformer(new YuiJsMinifier());
            var nullOrderer = new NullOrderer();

            BundleTable.Bundles.Add(new Bundle("~/CommonStyles", cssTransformer)
                {
                    Orderer = nullOrderer
                }.Include("~/Content/site.less",
                          "~/Content/themes/base/jquery-ui.css",
                          "~/Content/bootstrap.min.css",
                          "~/Content/bootstrap-responsive.min.css"));

            BundleTable.Bundles.Add(new Bundle("~/Modernizr", jsTransformer)
                {
                    Orderer = nullOrderer
                }.Include("~/Scripts/modernizr-2.5.3.js"));

            BundleTable.Bundles.Add(new Bundle("~/CommonScripts", jsTransformer)
                {
                    Orderer = nullOrderer
                }.Include("~/Scripts/jquery-1.7.2.min.js",
                          "~/Scripts/jquery-ui-1.8.20.min.js",
                          "~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.validate.unobtrusive.min.js",
                          "~/Scripts/jquery.unobtrusive-ajax.min.js",
                          "~/Scripts/bootstrap.min.js"));

            return TaskContinuation.Continue;
        }
    }
}