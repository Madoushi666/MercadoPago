using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CustomerCards.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface ICustomerCardsApplication
    {
        Task<BaseResponse<object>> GetCustomerCardsAsync(string customerId);
        Task<BaseResponse<object>> GetCustomerCardByIdAsync(string customerId, string cardId);
        Task<BaseResponse<object>> CreateCustomerCardAsync(string customerId, CustomerCardRequest bodyRequest);
        Task<BaseResponse<object>> UpdateCustomerCardAsync(string customerId, string cardId, CustomerCardRequest bodyRequest);
        Task<BaseResponse<object>> DeleteCustomerCardAsync(string customerId, string cardId);
    }
}
