namespace Web
{
    using Infrastructure;
    using MvcExtensions;
    using MvcExtensions.Windsor;

    public class MvcApplication : WindsorMvcApplication
    {
        public MvcApplication()
        {
            Bootstrapper.BootstrapperTasks
                .Include<RegisterControllers>()
                .Include<RegisterModelMetadata>()
                .Include<RegisterRoutes>()
                .Include<RegisterBundles>();
        }
    }
}