﻿using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class IdentificationTypesApplication : IIdentificationTypesApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;

        public IdentificationTypesApplication(IHttpClientManagerApplication httpClientManagerApplication)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
        }

        public async Task<BaseResponse<T>> GetIdentificationTypes<T>()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "identification_types");

            var response = await _httpClientManagerApplication.SendAsync<T>(request);

            return response;
        }
    }
}
