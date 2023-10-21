using CustomerMicroService.Data.Services.Responses;

namespace CustomerMicroService.Data.Services
{
    public interface IBrasilApiService
    {
        Task<DocumentInfo> GetDocumentInfoAsync(string cpf);
    }
}
