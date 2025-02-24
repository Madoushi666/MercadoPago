﻿using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
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

        public async Task<BaseResponse<T>> SearchPaymentMethods<T>(SearchPaymentMethodsRequestFilters filters)
        {
            filters.PublicKey = _mercadoPagoSettings.Value.PublicKey;
            var request = new HttpRequestMessage(HttpMethod.Get, $"payment_methods/search{_serializer.SetQueryParams(filters)}");

            var response = await _httpClientManagerApplication.SendAsync<T>(request);

            return response;
        }

        public async Task<BaseResponse<T>> GetPaymentMethods<T>()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "payment_methods");

            var response = await _httpClientManagerApplication.SendAsync<T>(request);

            return response;
        }
    }
}
