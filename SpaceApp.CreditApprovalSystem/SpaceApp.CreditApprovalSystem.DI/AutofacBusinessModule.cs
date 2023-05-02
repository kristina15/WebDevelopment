using Autofac;
using SpaceApp.CreditApprovalSystem.BLL;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using SpaceApp.CreditApprovalSystem.DAL;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.ModelValidatorContracts;
using SpaceApp.CreditApprovalSystem.ModelValidators;

namespace SpaceApp.CreditApprovalSystem.DI;

public class AutofaqBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //logic
        builder.RegisterType<UserLogic>().As<IUserLogic>().SingleInstance();
        builder.RegisterType<PassportLogic>().As<IPassportLogic>().SingleInstance();
        builder.RegisterType<LoanLogic>().As<ILoanLogic>().SingleInstance();
        builder.RegisterType<ScanLogic>().As<IScanLogic>().SingleInstance();

        //dao
        builder.RegisterType<UserDao>().As<IUserDao>().SingleInstance();
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