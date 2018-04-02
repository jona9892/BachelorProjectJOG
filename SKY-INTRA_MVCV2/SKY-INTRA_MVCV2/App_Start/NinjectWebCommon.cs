[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SKY_INTRA_MVCV2.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SKY_INTRA_MVCV2.App_Start.NinjectWebCommon), "Stop")]

namespace SKY_INTRA_MVCV2.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using ServiceGateway.APIGateway.Abstraction;
    using ServiceGateway.APIGateway.Implementation;
    using ServiceGateway.MessagingGateway.Abstraction;
    using ServiceGateway.MessagingGateway.Implementation;
    using ServiceGateway.Model;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Gateways
            kernel.Bind<IEmployeeGateway>().To<EmployeeGateway>();
            kernel.Bind<IAPIGateway<FileImage>>().To<FileImageGateway>();
            kernel.Bind<IGuestGateway>().To<GuestGateway>();
            kernel.Bind<IAPIGateway<string>>().To<ImageGateway>();
            kernel.Bind<IInformationGateway>().To<InformationGateway>();
            kernel.Bind<IInfoscreenGateway>().To<InfoscreenGateway>();
            kernel.Bind<IAPIGateway<SmallFileImage>>().To<SmallFileImageGateway>();
            kernel.Bind<IMessagingGateway>().To<InfoscreenMessaging>();

            


        }
    }
}
