using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CardToken.Request;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CardToken.Response;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface ICardTokensApplication
    {
        Task<BaseResponse<CardTokenResponse>> CreateCardTokenAsync(CardTokenRequest bodyRequest);
    }
}
