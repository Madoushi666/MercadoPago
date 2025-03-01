using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.PaymentMethods.Request;
using MercadoPago.CheckoutAPI.Application.Serialization;
using MercadoPago.CheckoutAPI.Application.Settings;
using Microsoft.Extensions.Options;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class PaymentMethodsApplication : IPaymentMethodsApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;
        private readonly ISerializer _serializer;
        private readonly IOptions<MercadoPagoSettings> _mercadoPagoSettings;

        public PaymentMethodsApplication(IHttpClientManagerApplication httpClientManagerApplication, ISerializer serializer, IOptions<MercadoPagoSettings> mercadoPagoSettings)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
            _serializer = serializer;
            _mercadoPagoSettings = mercadoPagoSettings;
        }

        public async Task<BaseResponse<object>> SearchPaymentMethodsAsync(PaymentMethodsRequestFilters filters)
        {
            filters.PublicKey = _mercadoPagoSettings.Value.PublicKey;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"payment_methods/search{_serializer.SetQueryParams(filters)}");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> GetPaymentMethodsAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "payment_methods");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> GetInstallmentsAsync(PaymentMethodsRequestFilters filters)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"payment_methods/installments{_serializer.SetQueryParams(filters)}");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }
    }
}
