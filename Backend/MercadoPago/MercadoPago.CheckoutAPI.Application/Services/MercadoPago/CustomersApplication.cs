﻿using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CardToken.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Customers.Request;
using MercadoPago.CheckoutAPI.Application.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;
        private readonly ISerializer _serializer;

        public CustomersApplication(IHttpClientManagerApplication httpClientManagerApplication, ISerializer serializer)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
            _serializer = serializer;
        }

        public async Task<BaseResponse<T>> SearchCustomers<T>(CustomersRequestFilters filters)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"customers/search{_serializer.SetQueryParams(filters)}");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponse<T>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<T>> GetCustomerById<T>(string customerId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"customers/{customerId}");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponse<T>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<T>> CreateCustomer<T>(CustomerRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"customers");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponse<T>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<T>> UpdateCustomer<T>(string customerId, CustomerRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"customers/{customerId}");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponse<T>(httpResponse);
            return response;
        }


    }
}
