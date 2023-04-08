using System;
using System.Reflection;

namespace SCB.Surkova.CreditApprovalSystem.Api.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}