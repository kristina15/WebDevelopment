using FluentValidation;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace SCB.Surkova.CreditApprovalSystem.Api.Filters
{
    public class MyExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext cntxt)
        {
            var exceptionType = cntxt.Exception.GetType();

            if(exceptionType==typeof(ValidationException))
            {
                cntxt.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else if(exceptionType == typeof(ArgumentNullException))
            {
                cntxt.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                cntxt.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}