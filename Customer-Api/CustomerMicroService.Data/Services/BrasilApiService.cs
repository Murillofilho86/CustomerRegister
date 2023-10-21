using CustomerMicroService.Data.Services.Responses;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CustomerMicroService.Data.Services
{
    public class BrasilApiService : IBrasilApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<BrasilApiService> _logger;

        public BrasilApiService(HttpClient httpClient, JsonSerializerOptions serializerOptions, ILogger<BrasilApiService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = serializerOptions;
            _logger = logger;
        }

        public async Task<DocumentInfo> GetDocumentInfoAsync(string cpf)
        {
            using var response = await _httpClient.GetAsync(cpf);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Http status code from {cpf} is {StatusCode}", cpf, response.StatusCode);
            }

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<DocumentInfo>(_serializerOptions);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadFromJsonAsync<BrasilApiResponseError>(_serializerOptions);
                _logger.LogError("Bad Request to {Cpnj}. Response error: {Error}", error.Message);

                throw new HttpRequestException($"Erro ao consultar cpf {cpf}. {error.Message}", null, response.StatusCode);
            }

            _logger.LogError("Error while querying cpnj. The server returned {StatusCode}. body:{Body}", response.StatusCode, await response.Content.ReadAsStringAsync());
            throw new HttpRequestException($"Could not query the cpf", null, response.StatusCode);
        }
    }
}
