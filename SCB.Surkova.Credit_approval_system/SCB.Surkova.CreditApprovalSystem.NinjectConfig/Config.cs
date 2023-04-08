using System;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using SCB.Surkova.CreditApprovalSystem.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Hash.Interfaces;
using SCB.Surkova.CreditApprovalSystem.HashGenerator;
using SCB.Surkova.CreditApprovalSystem.Validation;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;

namespace SCB.Surkova.CreditApprovalSystem.NinjectConfig
{
    public static class Config
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IUserDAL>()
                .To<UserDAL>();

            kernel
                .Bind<IPassportDao>()
                .To<PassportDao>();

            kernel
                .Bind<ILoanDao>()
                .To<LoanDao>();

            kernel
                .Bind<IScanDao>()
                .To<ScanDao>();

            kernel
                .Bind<IPassportLogic>()
                .To<PassportLogic>();

            kernel
                .Bind<IUserLogic>()
                .To<UserLogic>();

            kernel
                .Bind<ILoanLogic>()
                .To<LoanLogic>();

            kernel
                .Bind<IScanLogic>()
                .To<ScanLogic>();

            kernel
                .Bind<IUserValidator>()
                .To<UserValidator>();

            kernel
                .Bind<IPassportValidator>()
                .To<PassportValidator>();

            kernel
                .Bind<ILoanValidator>()
                .To<LoanValidator>();

            kernel
                .Bind<IScanValidator>()
                .To<ScanValidator>();

            kernel
                .Bind<IHashing>()
                .To<Hashing>();
        }
    }
}
