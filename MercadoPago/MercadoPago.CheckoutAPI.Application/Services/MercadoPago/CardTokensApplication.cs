using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CardToken.Request;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CardToken.Response;
using MercadoPago.CheckoutAPI.Application.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Services.MercadoPago
{
    public class CardTokensApplication : ICardTokensApplication
    {
        private readonly IHttpClientManagerApplication _httpClientManagerApplication;
        private readonly ISerializer _serializer;

        public CardTokensApplication(IHttpClientManagerApplication httpClientManagerApplication, ISerializer serializer)
        {
            _httpClientManagerApplication = httpClientManagerApplication;
            _serializer = serializer;
        }

        // USAR ESTE SERVICIO SOLAMENTE PARA PRUEBAS, POR SEGURIDAD EL TOKEN DEBE GENERARSE EN EL FRONTEND APUNTANDO DIRECTAMENTE HACIA LA API DE MERCADO PAGO
        public async Task<BaseResponse<CardTokenResponse>> CreateCardTokenAsync(CardTokenRequest bodyRequest)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "card_tokens");
            _serializer.AddJsonBodyToContent(httpRequest, bodyRequest);
            var httpResponse = await _httpClientManagerApplication.SendWithRetryAsync(httpRequest);
            
            var response = await _httpClientManagerApplication.SetBaseResponseAsync<CardTokenResponse>(httpResponse);
            return response;
        }
    }
}
