using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
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

        public async Task<BaseResponse<object>> GetIdentificationTypesAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "identification_types");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }
    }
}
