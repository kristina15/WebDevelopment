[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SCB.Surkova.CreditApprovalSystem.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SCB.Surkova.CreditApprovalSystem.Api.App_Start.NinjectWebCommon), "Stop")]

namespace SCB.Surkova.CreditApprovalSystem.Api.App_Start
{

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(Common.NinjectConfig.Config.CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
    }
}