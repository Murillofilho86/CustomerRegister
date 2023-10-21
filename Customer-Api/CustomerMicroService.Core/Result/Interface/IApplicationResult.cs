using CustomerMicroService.Framework.Message;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerMicroService.Framework.Result.Interface
{
    public interface IApplicationResult<TResultMessage> : IActionResult
    {
        TResultMessage Result { get; set; }

        string Message { get; set; }

        List<string> Validations { get; set; }

        string Protocol { get; }

        HttpStatusCode HttpStatusCode { get; set; }

        BaseRequest Request { get; }

        bool AutoAssignHttpStatusCode { get; set; }
    }
}
