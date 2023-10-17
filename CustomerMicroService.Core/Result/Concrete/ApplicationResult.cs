using CustomerMicroService.Framework.Message;
using CustomerMicroService.Framework.Result.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;

namespace CustomerMicroService.Framework.Result.Concrete
{
    public class ApplicationResult<TResultMessage> : IApplicationResult<TResultMessage>, IActionResult
    {
        [DataMember(Name = "retorno")]
        public TResultMessage Result { get; set; }

        [DataMember(Name = "mensagem")]
        public string Message { get; set; }

        [DataMember(Name = "validacoes")]
        public List<string> Validations { get; set; } = new List<string>();


        [DataMember(Name = "protocolo")]
        public string Protocol { get; private set; }

        [IgnoreDataMember]
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;


        [IgnoreDataMember]
        public BaseRequest Request { get; set; }

        [IgnoreDataMember]
        public bool AutoAssignHttpStatusCode { get; set; } = true;


        public ApplicationResult()
        {
        }

        public ApplicationResult(BaseRequest request)
        {
            Request = request;
            SetProtocol();
        }

        public ApplicationResult<TResultMessage> Set(HttpStatusCode httpStatusCode, string msg)
        {
            Message = msg;
            HttpStatusCode = httpStatusCode;
            return this;
        }

        private void SetProtocol()
        {
            if (Request != null)
            {
                Protocol = Request.GetHeader("Protocolo");
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(this));
            context.HttpContext.Response.Headers.Add("content-type", "application/json");
            if (AutoAssignHttpStatusCode)
            {
                List<string> validations = Validations;
                if (validations != null && validations.Count > 0)
                {
                    SetToUnprocessableEntity("Unprocessable entity");
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode;
                }
            }

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode;
            
            await stringContent.CopyToAsync(context.HttpContext.Response.Body);
        }

        public ApplicationResult<TResultMessage> SetToUnprocessableEntity(string msg)
        {
            return Set(HttpStatusCode.UnprocessableEntity, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToOk(string msg = "Sucesso")
        {
            return Set(HttpStatusCode.OK, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToCreated(string msg = "Criado com sucesso.")
        {
            return Set(HttpStatusCode.Created, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToAccepted(string msg = "Em fila de processamento.")
        {
            return Set(HttpStatusCode.Accepted, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToBadRequest(string msg = "Request inválido")
        {
            return Set(HttpStatusCode.BadRequest, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToInternalServerError(string msg = "Erro de comunicação.")
        {
            return Set(HttpStatusCode.InternalServerError, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToForbidden(string msg = "O servidor recusou a requisição.")
        {
            return Set(HttpStatusCode.Forbidden, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToNotFound(string msg = "Recurso não encontrado no servidor.")
        {
            return Set(HttpStatusCode.NotFound, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToServiceUnavailable(string msg = "Serviço indisponível.")
        {
            return Set(HttpStatusCode.ServiceUnavailable, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToUnauthorized(string msg = "Não autorizado o acesso ao recurso.")
        {
            return Set(HttpStatusCode.Unauthorized, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToNotAcceptable(string msg = "Informação Authorization no header é obrigatório.")
        {
            return Set(HttpStatusCode.NotAcceptable, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToRequestTimeout(string msg = "Tempo limite atingido no request.")
        {
            return Set(HttpStatusCode.RequestTimeout, msg);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToGatewayTimeout(string msg = "Tempo limite atingido.")
        {
            return Set(HttpStatusCode.GatewayTimeout, msg);
        }
    }
}
