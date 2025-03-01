using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Users.Request;
using MercadoPago.CheckoutAPI.Application.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;
        private readonly ISerializer _serializer;

        public UsersApplication(IHttpClientManagerApplication httpClientManagerApplication, ISerializer serializer)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
            _serializer = serializer;

            _httpClientManagerApplication.RemoveVersionUrlBase();
        }


        public async Task<BaseResponse<object>> GetMyUserAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "users/me");
            var httpResponse = await _httpClientManagerApplication.SendAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }

        public async Task<BaseResponse<object>> CreateTestUserAsync(CreateTestUserRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "users/test");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);

            var response = await _httpClientManagerApplication.SetBaseResponseAsync<object>(httpResponse);
            return response;
        }
    }
}
