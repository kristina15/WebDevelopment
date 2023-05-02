using Autofac;
using SCB.Surkova.CreditApprovalSystem.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Validation;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;

namespace SCB.Surkova.Credit_approval_system.Common
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //logic
            builder.RegisterType<UserLogic>().As<IUserLogic>().SingleInstance();
            builder.RegisterType<PassportLogic>().As<IPassportLogic>().SingleInstance();
            builder.RegisterType<LoanLogic>().As<ILoanLogic>().SingleInstance();
            builder.RegisterType<ScanLogic>().As<IScanLogic>().SingleInstance();

            //dao
            builder.RegisterType<UserDAL>().As<IUserDAL>().SingleInstance();
            builder.RegisterType<PassportDao>().As<IPassportDao>().SingleInstance();
            builder.RegisterType<LoanDao>().As<ILoanDao>().SingleInstance();
            builder.RegisterType<ScanDao>().As<IScanDao>().SingleInstance();

            //validator
            builder.RegisterType<UserValidator>().As<IUserValidator>().SingleInstance();
            builder.RegisterType<PassportValidator>().As<IPassportValidator>().SingleInstance();
            builder.RegisterType<LoanValidator>().As<ILoanValidator>().SingleInstance();
            builder.RegisterType<ScanValidator>().As<IScanValidator>().SingleInstance();
        }
    }
}
