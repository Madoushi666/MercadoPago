using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Serialization;
using MercadoPago.CheckoutAPI.Application.Settings;
using MercadoPago.CheckoutAPI.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class HttpClientManagerApplication : IHttpClientManagerApplication
    {
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;
        private readonly ILogger<HttpClientManagerApplication> _logger;
        private readonly int _maxRetries;
        private readonly int _retryDelayMilliseconds;

        public HttpClientManagerApplication(IHttpClientFactory httpClientFactory, ISerializer serializer, ILogger<HttpClientManagerApplication> logger, IOptions<MercadoPagoSettings> mercadoPagoSettings)
        {
            _httpClient = httpClientFactory.CreateClient("MercadoPagoHttpClient");
            _logger = logger;
            _serializer = serializer;
            _maxRetries = Convert.ToInt32(mercadoPagoSettings.Value.MaxRetriesHTTP);
            _retryDelayMilliseconds = Convert.ToInt32(mercadoPagoSettings.Value.RetryDelayMilliseconds);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                _logger.LogInformation("{TimeStamp} IN Request: {BaseAddress}{RequestUri}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), _httpClient.BaseAddress, request.RequestUri);
                httpResponseMessage = await _httpClient.SendAsync(request);
                _logger.LogInformation("{TimeStamp} OUT Request: {RequestUri}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), request.RequestUri);
            }
            catch (Exception ex)
            {
                _logger.LogError("{TimeStamp} ERROR: {Exception}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex);
            }

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> SendWithRetryAsync(HttpRequestMessage request)
        {
            int attempts = 0;
            var httpResponseMessage = new HttpResponseMessage();

            while (attempts < _maxRetries)
            {
                attempts++;
                try
                {
                    HttpRequestMessage attemptRequest = await CloneRequestAsync(request);
                    _logger.LogInformation("{TimeStamp} IN Request: (Intento Nro {Attempts}): {BaseAddress}{RequestUri}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), attempts, _httpClient.BaseAddress, attemptRequest.RequestUri);
                    httpResponseMessage = await _httpClient.SendAsync(attemptRequest);
                    _logger.LogInformation("{TimeStamp} OUT Request: (Intento Nro {Attempts}): {RequestUri}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), attempts, attemptRequest.RequestUri);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return httpResponseMessage;
                    }

                    if (httpResponseMessage.StatusCode == HttpStatusCode.RequestTimeout || (int)httpResponseMessage.StatusCode >= 500)
                    {
                        await Task.Delay(_retryDelayMilliseconds);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{TimeStamp} (Intento Nro {Attempts}) ERROR: {Exception}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), attempts, ex);
                    if (attempts < _maxRetries)
                    {
                        await Task.Delay(_retryDelayMilliseconds);
                    }
                }
            }

            return httpResponseMessage;
        }

        public async Task<BaseResponse<T>> SetBaseResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            var response = new BaseResponse<T>();

            response.Message = ReplyMessages.SetStatusCodeMessage((int)httpResponse.StatusCode);
            response.StatusCode = (int)httpResponse.StatusCode;
            response.Data = await _serializer.DeserializeJsonAsync<T>(httpResponse);

            return response;
        }

        public HttpRequestHeaders AddXIdempotencyKey(HttpRequestHeaders headers)
        {
            string X_IDEMPOTENCY_KEY = "X-Idempotency-Key";

            headers.Add(X_IDEMPOTENCY_KEY, Guid.NewGuid().ToString());

            return headers;
        }

        public void RemoveVersionUrlBase()
        {
            _httpClient.BaseAddress = new Uri(_httpClient.BaseAddress.ToString().Replace("/v1", ""));
        }

        private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Content = request.Content is not null ? await CloneContentAsync(request.Content).ConfigureAwait(false) : null,
                Version = request.Version
            };

            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }

        private static async Task<HttpContent> CloneContentAsync(HttpContent content)
        {
            if (content is null)
                return null!;

            var memoryStream = new MemoryStream();
            await content.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;

            HttpContent clone = content switch
            {
                StringContent => new StringContent(await content.ReadAsStringAsync().ConfigureAwait(false), Encoding.UTF8, "application/json"),
                ByteArrayContent => new ByteArrayContent(await content.ReadAsByteArrayAsync().ConfigureAwait(false)),
                _ => new StreamContent(memoryStream)
            };

            foreach (var header in content.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }
    }
}
