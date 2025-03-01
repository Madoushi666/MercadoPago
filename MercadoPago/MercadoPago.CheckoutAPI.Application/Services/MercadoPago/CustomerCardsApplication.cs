using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CustomerCards.Request;
using MercadoPago.CheckoutAPI.Application.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class CustomerCardsApplication : ICustomerCardsApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;
        private readonly ISerializer _serializer;

        public CustomerCardsApplication(IHttpClientManagerApplication httpClientManagerApplication, ISerializer serializer)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
            _serializer = serializer;
        }

        public async Task<BaseResponse<object>> GetCustomerCardsAsync(string customerId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"customers/{customerId}/cards");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> GetCustomerCardByIdAsync(string customerId, string cardId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"customers/{customerId}/cards/{cardId}");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> CreateCustomerCardAsync(string customerId, CustomerCardRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"customers/{customerId}/cards");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> UpdateCustomerCardAsync(string customerId, string cardId, CustomerCardRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"customers/{customerId}/cards/{cardId}");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> DeleteCustomerCardAsync(string customerId, string cardId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"customers/{customerId}/cards/{cardId}");
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }
    }
}
